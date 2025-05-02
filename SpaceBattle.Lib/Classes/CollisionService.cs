using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionService : ICommand
{
    public void Execute()
    {
        var grid = IoC.Resolve<ISpatialPartitionGrid>("Game.SpatialGrid");

        var commands = grid.GetAllObjects()
            .Select(obj => IoC.Resolve<ICommand>("Game.CollisionCheckCommand", obj))
            .ToList();

        IoC.Resolve<ICommand>("Commands.Macro", commands).Execute();
    }
}
