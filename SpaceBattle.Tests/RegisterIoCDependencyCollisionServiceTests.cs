using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyCollisionServiceTests
{
    public RegisterIoCDependencyCollisionServiceTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void CollisionServiceRegisteredPositiveTest()
    {
        new RegisterIoCDependencyCollisionService().Execute();

        var mockGrid = new Mock<ISpatialPartitionGrid>();

        var service = IoC.Resolve<ICommand>("Game.CollisionService", mockGrid.Object);

        Assert.NotNull(service);
        Assert.IsType<CollisionService>(service);
    }
}
