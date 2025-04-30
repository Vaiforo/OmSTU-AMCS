using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionService : ICommand
{
    public void Execute()
    {
        var grid = IoC.Resolve<ISpatialPartitionGrid>("Game.SpatialGrid");
        var allObjects = grid.GetAllOccupiedCells().SelectMany(grid.GetObjectsInCell).Distinct();
        var commands = allObjects
            .SelectMany(obj =>
                (IEnumerable<ICommand>)IoC.Resolve<object>("Game.CollisionCheckCommandFactory", obj)
            )
            .ToList();
        new MacroCommand(commands).Execute();
    }
}
