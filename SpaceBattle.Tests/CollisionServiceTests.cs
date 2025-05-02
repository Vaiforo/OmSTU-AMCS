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
        var objects = new List<IMovingObject> { obj1, obj2 };

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock.Setup(g => g.GetAllObjects()).Returns(objects);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => gridMock.Object)
            )
            .Execute();

        var checkCommands = new List<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.CollisionCheckCommand",
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
    public void CollisionServiceNoObjectsTest()
    {
        var objects = new List<IMovingObject>();

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock.Setup(g => g.GetAllObjects()).Returns(objects);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => gridMock.Object)
            )
            .Execute();

        var checkCommands = new List<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.CollisionCheckCommand",
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
    public void CollisionServiceSingleObjectTest()
    {
        var obj1 = new MockMovingObject([15, 25]);
        var objects = new List<IMovingObject> { obj1 };

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock.Setup(g => g.GetAllObjects()).Returns(objects);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => gridMock.Object)
            )
            .Execute();

        var checkCommands = new List<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.CollisionCheckCommand",
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
