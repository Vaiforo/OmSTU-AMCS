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
    public void CollisionCheckCommand_CollisionDetected_PrintsCollisionMessage()
    {
        var mockObj = new Mock<IMovingObject>();
        var mockOther = new Mock<IMovingObject>();

        mockObj.Setup(m => m.ToString()).Returns("ObjectA");
        mockOther.Setup(m => m.ToString()).Returns("ObjectB");

        var mockGrid = new Mock<ISpatialPartitionGrid>();
        mockGrid
            .Setup(g => g.GetNearby(mockObj.Object))
            .Returns(new List<IMovingObject> { mockObj.Object, mockOther.Object });

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => mockGrid.Object)
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(args => true)
            )
            .Execute();

        var command = new CollisionCheckCommand(mockObj.Object);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        command.Execute();

        var output = sw.ToString();
        Assert.Contains("ObjectA collided with ObjectB", output);
    }

    [Fact]
    public void CollisionCheckCommand_NoCollision_NoOutput()
    {
        var mockObj = new Mock<IMovingObject>();
        var mockOther = new Mock<IMovingObject>();

        mockObj.Setup(m => m.ToString()).Returns("ObjectA");
        mockOther.Setup(m => m.ToString()).Returns("ObjectB");

        var mockGrid = new Mock<ISpatialPartitionGrid>();
        mockGrid
            .Setup(g => g.GetNearby(mockObj.Object))
            .Returns(new List<IMovingObject> { mockObj.Object, mockOther.Object });

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                new Func<object[], object>(args => mockGrid.Object)
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Grid.CollisionDetector",
                new Func<object[], object>(args => false)
            )
            .Execute();

        var command = new CollisionCheckCommand(mockObj.Object);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        command.Execute();

        var output = sw.ToString();
        Assert.DoesNotContain("collided", output);
    }
}
