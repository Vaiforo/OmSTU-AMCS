using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CollisionCheckCommandTests
{
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
    public void CollisionDetected_ExecutesCollisionHandler()
    {
        var objA = new Mock<IMovingObject>();
        var objB = new Mock<IMovingObject>();

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock
            .Setup(g => g.GetNearby(objA.Object))
            .Returns(new List<IMovingObject> { objA.Object, objB.Object });

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(_ => gridMock.Object)
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(_ => true)
            )
            .Execute();

        var handlerMock = new Mock<ICommand>();
        handlerMock.Setup(h => h.Execute());

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionHandler",
                new Func<object[], object>(args =>
                {
                    Assert.Equal(objA.Object, args[0]);
                    Assert.Equal(objB.Object, args[1]);
                    return handlerMock.Object;
                })
            )
            .Execute();

        var command = new CollisionCheckCommand(objA.Object);

        command.Execute();

        handlerMock.Verify(h => h.Execute(), Times.Once);
    }

    [Fact]
    public void NoCollision_CollisionHandlerNotExecuted()
    {
        var objA = new Mock<IMovingObject>();
        var objB = new Mock<IMovingObject>();

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock
            .Setup(g => g.GetNearby(objA.Object))
            .Returns(new List<IMovingObject> { objA.Object, objB.Object });

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(_ => gridMock.Object)
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(_ => false)
            )
            .Execute();

        var handlerMock = new Mock<ICommand>();
        handlerMock.Setup(h => h.Execute());

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionHandler",
                new Func<object[], object>(_ => handlerMock.Object)
            )
            .Execute();

        var command = new CollisionCheckCommand(objA.Object);

        command.Execute();

        handlerMock.Verify(h => h.Execute(), Times.Never);
    }
}
