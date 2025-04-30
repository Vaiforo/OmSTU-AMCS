using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionCheckCommand : ICommand
{
    private readonly IMovingObject _obj;

    public CollisionCheckCommand(IMovingObject obj)
    {
        _obj = obj;
    }

    public void Execute()
    {
        var grid = IoC.Resolve<ISpatialPartitionGrid>("Game.SpatialGrid");

        var nearby = grid.GetNearby(_obj);

        foreach (var other in nearby)
        {
            if (other == _obj)
            {
                continue;
            }

            if (IoC.Resolve<bool>("Grid.CollisionDetector", $"{_obj}.{other}"))
            {
                IoC.Resolve<ICommand>("Grid.CollisionHandler", _obj, other).Execute();
            }
        }
    }
}
