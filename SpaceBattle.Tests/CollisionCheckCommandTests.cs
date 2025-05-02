using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CollisionCheckCommandTests
{
    private class MockMovingObject(int[] coords) : IMovingObject
    {
        public Vector Position { get; set; } = new Vector(coords);
        public Vector Velocity { get; } = new Vector(0, 0);

        public override string ToString() => $"MockMovingObject_{GetHashCode()}";
    }

    public CollisionCheckCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void CollisionCheckPositiveTest()
    {
        var obj1 = new MockMovingObject([15, 25]);
        var obj2 = new MockMovingObject([16, 26]);
        var nearbyObjects = new List<IMovingObject> { obj2 };

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock.Setup(g => g.GetNearby(obj1)).Returns(nearbyObjects);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => gridMock.Object)
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(args => (string)args[0] == $"{obj1}.{obj2}")
            )
            .Execute();

        var handlerMock = new Mock<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionHandler",
                new Func<object[], object>(args =>
                    args[0] == obj1 && args[1] == obj2
                        ? handlerMock.Object
                        : throw new InvalidOperationException()
                )
            )
            .Execute();

        var cmd = new CollisionCheckCommand(obj1);

        cmd.Execute();

        handlerMock.Verify(h => h.Execute(), Times.Once());
    }

    [Fact]
    public void CollisionCheckNoCollisionPositiveTest()
    {
        var obj1 = new MockMovingObject([15, 25]);
        var obj2 = new MockMovingObject([16, 26]);
        var nearbyObjects = new List<IMovingObject> { obj2 };

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock.Setup(g => g.GetNearby(obj1)).Returns(nearbyObjects);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => gridMock.Object)
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(args => false)
            )
            .Execute();

        var handlerMock = new Mock<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionHandler",
                new Func<object[], object>(args => handlerMock.Object)
            )
            .Execute();

        var cmd = new CollisionCheckCommand(obj1);

        cmd.Execute();

        handlerMock.Verify(h => h.Execute(), Times.Never());
    }

    [Fact]
    public void CollisionCheckNoNearbyObjectsPositiveTest()
    {
        var obj1 = new MockMovingObject([15, 25]);
        var nearbyObjects = new List<IMovingObject>();

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock.Setup(g => g.GetNearby(obj1)).Returns(nearbyObjects);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => gridMock.Object)
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(args => false)
            )
            .Execute();

        var handlerMock = new Mock<ICommand>();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionHandler",
                new Func<object[], object>(args => handlerMock.Object)
            )
            .Execute();

        var cmd = new CollisionCheckCommand(obj1);

        cmd.Execute();

        handlerMock.Verify(h => h.Execute(), Times.Never());
    }
}
