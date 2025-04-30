using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionService : ICommand
{
    public void Execute()
    {
        var grid = IoC.Resolve<ISpatialPartitionGrid>("Game.SpatialGrid");

        var allObjects = grid.GetAllOccupiedCells().SelectMany(grid.GetObjectsInCell).Distinct();

        var commands = allObjects.Select(obj => new CollisionCheckCommand(obj)).ToList();

        new MacroCommand(commands).Execute();
    }
}
