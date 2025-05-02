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

        grid.GetNearby(_obj)
            .Where(other => IoC.Resolve<bool>("Grid.CollisionDetector", $"{_obj}.{other}"))
            .Select(other => IoC.Resolve<ICommand>("Grid.CollisionHandler", _obj, other))
            .ToList()
            .ForEach(cmd => cmd.Execute());
    }
}
