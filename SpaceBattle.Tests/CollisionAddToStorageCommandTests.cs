using Moq;
using SpaceBattle.Lib;

public class CollisionStorageTests
{
    [Fact]
    public void StoreCollision_AddsCollisionsToStorage()
    {
        var storageDict = new Dictionary<(string, string), List<(int, int, int, int)>>();
        var storage = new CollisionStorage(storageDict);
        var collisions = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };

        storage.StoreCollision("ship", "asteroid", collisions);

        Assert.True(storageDict.ContainsKey(("ship", "asteroid")));
        Assert.Equal(collisions, storageDict[("ship", "asteroid")]);
    }

    [Fact]
    public void StoreCollision_OverwritesExistingCollisions()
    {
        var storageDict = new Dictionary<(string, string), List<(int, int, int, int)>>();
        storageDict[("ship", "asteroid")] = new List<(int, int, int, int)> { (0, 0, 0, 0) };
        var storage = new CollisionStorage(storageDict);
        var newCollisions = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };

        storage.StoreCollision("ship", "asteroid", newCollisions);

        Assert.Equal(newCollisions, storageDict[("ship", "asteroid")]);
    }

    [Fact]
    public void TryGetCollisions_ReturnsTrueAndCollisions_WhenExists()
    {
        var storageDict = new Dictionary<(string, string), List<(int, int, int, int)>>();
        var expectedCollisions = new List<(int, int, int, int)> { (1, 2, 3, 4) };
        storageDict[("ship", "asteroid")] = expectedCollisions;
        var storage = new CollisionStorage(storageDict);

        var result = storage.TryGetCollisions("ship", "asteroid", out var retrievedCollisions);

        Assert.True(result);
        Assert.Equal(expectedCollisions, retrievedCollisions);
    }

    [Fact]
    public void TryGetCollisions_ReturnsFalseAndNull_WhenNotExists()
    {
        var storageDict = new Dictionary<(string, string), List<(int, int, int, int)>>();
        var storage = new CollisionStorage(storageDict);

        var result = storage.TryGetCollisions("ship", "asteroid", out var retrievedCollisions);

        Assert.False(result);
        Assert.Null(retrievedCollisions);
    }
    [Fact]
    public void Execute_CallStoreCollision_WithCorrectParams()
    {
        var storageMock = new Mock<ICollisionStorage>();
        var collisions = new List<(int, int, int, int)> { (1, 2, 3, 4) };
        var command = new CollisionAddToStorageCommand("ship", "asteroid", collisions, storageMock.Object);

        command.Execute();

        storageMock.Verify(s => s.StoreCollision("ship", "asteroid", collisions), Times.Once);
    }
}
