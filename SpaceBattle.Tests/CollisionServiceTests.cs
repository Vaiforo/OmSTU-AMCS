using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CollisionServiceTests
{
    private class MockMovingObject(int[] coords) : IMovingObject
    {
        public Vector Position { get; set; } = new Vector(coords);
        public Vector Velocity { get; } = new Vector(0, 0);
    }

    public CollisionServiceTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void CollisionServicePositiveTest()
    {
        var obj1 = new MockMovingObject([15, 25]);
        var obj2 = new MockMovingObject([35, 45]);
        var grid = new SpatialPartitionGrid(10, 2);
        grid.AddToGrid(obj1);
        grid.AddToGrid(obj2);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => grid)
            )
            .Execute();

        var checkCommands = new List<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.CollisionCheckCommand",
                new Func<object[], object>(args =>
                {
                    var cmdMock = new Mock<ICommand>();
                    checkCommands.Add(cmdMock.Object);
                    return cmdMock.Object;
                })
            )
            .Execute();

        var macroCommandMock = new Mock<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Macro",
                new Func<object[], object>(args =>
                {
                    var commands = (IEnumerable<ICommand>)args[0];
                    return commands.Any() ? macroCommandMock.Object : new Mock<ICommand>().Object;
                })
            )
            .Execute();

        var cmd = new CollisionService();

        cmd.Execute();

        Assert.Equal(2, checkCommands.Count);
        macroCommandMock.Verify(m => m.Execute(), Times.Once());
    }

    [Fact]
    public void CollisionServiceNoObjectsPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => grid)
            )
            .Execute();

        var checkCommands = new List<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.CollisionCheckCommand",
                new Func<object[], object>(args =>
                {
                    var cmdMock = new Mock<ICommand>();
                    checkCommands.Add(cmdMock.Object);
                    return cmdMock.Object;
                })
            )
            .Execute();

        var macroCommandMock = new Mock<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Macro",
                new Func<object[], object>(args =>
                {
                    var commands = (IEnumerable<ICommand>)args[0];
                    return commands.Any() ? macroCommandMock.Object : new Mock<ICommand>().Object;
                })
            )
            .Execute();

        var cmd = new CollisionService();

        cmd.Execute();

        Assert.Empty(checkCommands);
        macroCommandMock.Verify(m => m.Execute(), Times.Never());
    }

    [Fact]
    public void CollisionServiceSingleObjectPositiveTest()
    {
        var obj1 = new MockMovingObject([15, 25]);
        var grid = new SpatialPartitionGrid(10, 2);
        grid.AddToGrid(obj1);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => grid)
            )
            .Execute();

        var checkCommands = new List<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.CollisionCheckCommand",
                new Func<object[], object>(args =>
                {
                    var cmdMock = new Mock<ICommand>();
                    checkCommands.Add(cmdMock.Object);
                    return cmdMock.Object;
                })
            )
            .Execute();

        var macroCommandMock = new Mock<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Macro",
                new Func<object[], object>(args =>
                {
                    var commands = (IEnumerable<ICommand>)args[0];
                    return commands.Any() ? macroCommandMock.Object : new Mock<ICommand>().Object;
                })
            )
            .Execute();

        var cmd = new CollisionService();

        cmd.Execute();

        Assert.Single(checkCommands);
        macroCommandMock.Verify(m => m.Execute(), Times.Once());
    }
}
