namespace SpaceBattle.Lib;

public class CollisionStorage : ICollisionStorage
{
    private readonly Dictionary<(string, string), List<(int, int, int, int)>> _storage;

    public CollisionStorage(Dictionary<(string, string), List<(int, int, int, int)>> storage)
    {
        _storage = storage;
    }

    public void StoreCollision(string form1, string form2, List<(int, int, int, int)> collisions)
    {
        _storage[(form1, form2)] = collisions;
    }

    public bool TryGetCollisions(string form1, string form2, out List<(int, int, int, int)> collisions)
    {
        return _storage.TryGetValue((form1, form2), out collisions!);
    }
}