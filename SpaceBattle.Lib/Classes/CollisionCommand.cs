using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionCommand : ICommand
{
    private readonly object _object1;

    private readonly object _object2;

    public CollisionCommand(object object1, object object2)
    {
        _object1 = object1;
        _object2 = object2;
    }

    public void Execute()
    {
        if (IoC.Resolve<bool>("Collision.Check", _object1, _object2))
        {
            IoC.Resolve<ICommand>("Collision.Handle", _object1, _object2).Execute();
        }
    }
}
