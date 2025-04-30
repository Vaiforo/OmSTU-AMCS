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
    public void CollisionServiceExecutesCollisionCheckCommandsForAllObjects()
    {
        var mockGrid = new Mock<ISpatialPartitionGrid>();
        var mockObj = new Mock<IMovingObject>();
        var mockOther = new Mock<IMovingObject>();

        mockGrid
            .Setup(g => g.GetNearby(mockObj.Object))
            .Returns(new List<IMovingObject> { mockObj.Object, mockOther.Object });

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => mockGrid.Object)
            )
            .Execute();

        // Вот сюда вставляешь заглушку для "Grid.CollisionDetector"
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(args => true)
            )
            .Execute();

        var cmd = new CollisionCheckCommand(mockObj.Object);

        var writer = new StringWriter();
        Console.SetOut(writer); // перехват вывода

        cmd.Execute();

        var output = writer.ToString();
        Assert.Contains("collided", output); // Проверка, что сработал вывод
    }
}
