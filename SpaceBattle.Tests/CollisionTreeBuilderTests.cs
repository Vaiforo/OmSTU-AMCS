using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

public class CollisionTreeBuilderTests
{
    private readonly Mock<ICollisionStorage> _storageMock;

    public CollisionTreeBuilderTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
        _storageMock = new Mock<ICollisionStorage>();
        IoC.Resolve<ICommand>("IoC.Register", "Collisions.Storage", (object[] args) => _storageMock.Object).Execute();
    }

    [Fact]
    public void BuildCollisionTree_ReturnsCollisionsFromStorage_WhenExists()
    {
        var expectedCollisions = new List<(int, int, int, int)> { (1, 2, 3, 4) };
        _storageMock.Setup(s => s.TryGetCollisions("ship", "asteroid", out expectedCollisions)).Returns(true);

        var result = CollisionTreeBuilder.BuildCollisionTree("ship", "asteroid");

        Assert.Equal("ship", result.movingObject);
        Assert.Equal("asteroid", result.staticObject);
        Assert.Equal(expectedCollisions, result.collisions);
        _storageMock.Verify(s => s.StoreCollision(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<(int, int, int, int)>>()), Times.Never());
    }

    [Fact]
    public void BuildCollisionTree_LoadsCollisionsFromFile_WhenInStorage()
    {
        List<(int, int, int, int)>? collisions = null;
        _storageMock.Setup(s => s.TryGetCollisions("ship", "asteroid", out collisions)).Returns(false);
        var directory = "game_collisions";
        Directory.CreateDirectory(directory);
        var filePath = Path.Combine(directory, "ship_asteroid_collision.txt");
        File.WriteAllLines(filePath, new[] { "1,2,3,4", "5,6,7,8" });
        var expectedCollisions = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };

        var result = CollisionTreeBuilder.BuildCollisionTree("ship", "asteroid");

        Assert.Equal("ship", result.movingObject);
        Assert.Equal("asteroid", result.staticObject);
        Assert.Equal(expectedCollisions, result.collisions);
        _storageMock.Verify(s => s.StoreCollision("ship", "asteroid", expectedCollisions), Times.Once());

        File.Delete(filePath);
    }

    [Fact]
    public void BuildCollisionTree_LoadsCollisionsFromFile_WhenNotInStorage()
    {
        List<(int, int, int, int)>? collisions = null;
        _storageMock.Setup(s => s.TryGetCollisions("ship", "asteroid", out collisions)).Returns(false);
        var directory = "game_collisions";
        Directory.CreateDirectory(directory);
        var filePath = Path.Combine(directory, "ship_asteroid_collision.txt");

        var result = CollisionTreeBuilder.BuildCollisionTree("ship", "asteroid");

        Assert.Equal("ship", result.movingObject);
        Assert.Equal("asteroid", result.staticObject);

        Assert.False(File.Exists(filePath));

    }
}
