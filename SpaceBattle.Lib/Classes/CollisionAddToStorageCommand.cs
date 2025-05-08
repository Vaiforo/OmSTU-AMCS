using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionAddToStorageCommand : ICommand
{
    private readonly string _form1;
    private readonly string _form2;
    private readonly List<(int, int, int, int)> _collisions;
    private readonly Dictionary<(string, string), List<(int, int, int, int)>> _storage;

    public CollisionAddToStorageCommand(object[] args)
    {
        _form1 = (string)args[0];
        _form2 = (string)args[1];
        _collisions = (List<(int, int, int, int)>)args[2];
        _storage = (Dictionary<(string, string), List<(int, int, int, int)>>)args[3];
    }

    public void Execute()
    {
        _storage[(_form1, _form2)] = _collisions;
    }
}
