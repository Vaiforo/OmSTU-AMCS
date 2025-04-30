using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencySpatialPartitionGridTests
{
    public RegisterIoCDependencySpatialPartitionGridTests()
    {
        // Инициализируем IoC и создаём новый скоуп
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void SpatialPartitionGridRegisteredPositiveTest()
    {
        new RegisterIoCDependencySpatialPartitionGrid().Execute();

        var grid = IoC.Resolve<SpatialPartitionGrid>("Game.SpatialGrid", 10.0, 2);

        Assert.NotNull(grid);
        Assert.IsType<SpatialPartitionGrid>(grid);
    }
}
