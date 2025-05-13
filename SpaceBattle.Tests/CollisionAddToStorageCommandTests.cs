using SpaceBattle.Lib;

public class CollisionAddToStorageCommandTests
{
    [Fact]
    public void Execute_AddsCollisionsToStorage()
    {
        var storageDict = new Dictionary<(string, string), List<(int, int, int, int)>>();
        var storage = new CollisionStorage(storageDict);
        var collisions = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };
        var command = new CollisionAddToStorageCommand("ship", "asteroid", collisions, storage);

        command.Execute();

        Assert.True(storageDict.ContainsKey(("ship", "asteroid")));
        Assert.Equal(collisions, storageDict[("ship", "asteroid")]);
    }

    [Fact]
    public void Execute_OverwritesExistingKey()
    {
        var storageDict = new Dictionary<(string, string), List<(int, int, int, int)>>();
        storageDict[("ship", "asteroid")] = new List<(int, int, int, int)> { (0, 0, 0, 0) };
        var storage = new CollisionStorage(storageDict);
        var newCollisions = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };
        var command = new CollisionAddToStorageCommand("ship", "asteroid", newCollisions, storage);

        command.Execute();

        Assert.Equal(newCollisions, storageDict[("ship", "asteroid")]);
    }

    [Fact]
    public void Execute_UsesInterfaceCorrectly()
    {
        var storageDict = new Dictionary<(string, string), List<(int, int, int, int)>>();
        var storage = new CollisionStorage(storageDict);
        var collisions = new List<(int, int, int, int)> { (1, 2, 3, 4) };
        var command = new CollisionAddToStorageCommand("ship", "asteroid", collisions, storage);

        command.Execute();

        Assert.Single(storageDict);
        Assert.Equal(collisions, storageDict[("ship", "asteroid")]);
    }
}
