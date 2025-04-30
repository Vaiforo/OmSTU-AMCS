using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionCommand : ICommand
{
    private readonly IMovingObject _object1;

    private readonly IMovingObject _object2;

    public CollisionCommand(IMovingObject object1, IMovingObject object2)
    {
        _object1 = object1;
        _object2 = object2;
    }

    public void Execute()
    {
        var deltaValues = IoC.Resolve<Array>("GetDeltaValues", _object1, _object2);

        if(IoC.Resolve<bool>("Collision.Check", deltaValues))
        {
            IoC.Resolve<ICommand>("Collision.Handle", _object1, _object2).Execute();
        }
    }
}