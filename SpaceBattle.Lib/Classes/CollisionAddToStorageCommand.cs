using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionAddToStorageCommand : ICommand
{
    private readonly string _form1;
    private readonly string _form2;
    private readonly List<(int, int, int, int)> _collisions;
    private readonly ICollisionStorage _storage;

    public CollisionAddToStorageCommand(
        string form1,
        string form2,
        List<(int, int, int, int)> collisions,
        ICollisionStorage storage
    )
    {
        _form1 = form1;
        _form2 = form2;
        _collisions = collisions;
        _storage = storage;
    }

    public void Execute()
    {
        _storage.StoreCollision(_form1, _form2, _collisions);
    }
}
