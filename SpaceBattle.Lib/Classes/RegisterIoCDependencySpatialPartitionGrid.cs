using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencySpatialPartitionGrid : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.SpatialGrid",
                (object[] args) =>
                {
                    var cellSize = (double)args[0];
                    var dimensions = (int)args[1];

                    return new SpatialPartitionGrid(cellSize, dimensions);
                }
            )
            .Execute();
    }
}
