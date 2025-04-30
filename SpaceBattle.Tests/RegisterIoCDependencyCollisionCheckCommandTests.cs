using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyCollisionCheckCommandTests
{
    public RegisterIoCDependencyCollisionCheckCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void CollisionCheckCommandRegisteredPositiveTest()
    {
        new RegisterIoCDependencyCollisionCheckCommand().Execute();

        var mockObject = new Mock<IMovingObject>();

        var command = IoC.Resolve<ICommand>("Game.CollisionCheckCommand", mockObject.Object);

        Assert.NotNull(command);
        Assert.IsType<CollisionCheckCommand>(command);
    }
}
