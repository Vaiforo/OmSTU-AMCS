using SpaceBattle.Lib;

public class CollisionAddToStorageCommandTests
{
    [Fact]
    public void Execute_AddsCollisionsToStorage()
    {
        var storage = new Dictionary<(string, string), List<(int, int, int, int)>>();
        var collisions = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };
        var command = new CollisionAddToStorageCommand(
            new object[] { "ship", "asteroid", collisions, storage }
        );

        command.Execute();

        Assert.True(storage.ContainsKey(("ship", "asteroid")));
        Assert.Equal(collisions, storage[("ship", "asteroid")]);
    }

    [Fact]
    public void Execute_OverwritesExistingKey()
    {
        var storage = new Dictionary<(string, string), List<(int, int, int, int)>>();
        storage[("ship", "asteroid")] = new List<(int, int, int, int)> { (0, 0, 0, 0) };
        var newCollisions = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };
        var command = new CollisionAddToStorageCommand(
            new object[] { "ship", "asteroid", newCollisions, storage }
        );

        command.Execute();

        Assert.Equal(newCollisions, storage[("ship", "asteroid")]);
    }

    [Fact]
    public void Constructor_InvalidArgs_ThrowsException()
    {
        var invalidArgs = new object[]
        {
            123,
            "asteroid",
            new List<(int, int, int, int)>(),
            new Dictionary<(string, string), List<(int, int, int, int)>>(),
        };

        Assert.Throws<InvalidCastException>(() => new CollisionAddToStorageCommand(invalidArgs));
    }
}
