using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CollisionServiceTests
{
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
    public void CollisionServiceExecutesCollisionHandlers()
    {
        var obj1 = new Mock<IMovingObject>();
        obj1.SetupGet(o => o.Position).Returns(new Vector(0, 0));

        var obj2 = new Mock<IMovingObject>();
        obj2.SetupGet(o => o.Position).Returns(new Vector(1, 1));

        var allObjects = new List<IMovingObject> { obj1.Object, obj2.Object };

        var gridMock = new Mock<ISpatialPartitionGrid>();
        gridMock.Setup(g => g.GetAllOccupiedCells()).Returns(new List<int[]> { new[] { 0, 0 } });
        gridMock.Setup(g => g.GetObjectsInCell(It.IsAny<int[]>())).Returns(allObjects);
        gridMock.Setup(g => g.GetNearby(It.IsAny<IMovingObject>())).Returns(allObjects);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(_ => gridMock.Object)
            )
            .Execute();

        // Mock для коллизий — просто true
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(_ => true)
            )
            .Execute();

        // Handler, который мы будем проверять
        var handlerMock = new Mock<ICommand>();
        handlerMock.Setup(h => h.Execute());

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionHandler",
                new Func<object[], object>(_ => handlerMock.Object)
            )
            .Execute();

        var cmd = new CollisionService();
        cmd.Execute();

        // Должен вызваться дважды: (obj1,obj2) и (obj2,obёj1)
        handlerMock.Verify(h => h.Execute(), Times.Exactly(2));
    }
}
