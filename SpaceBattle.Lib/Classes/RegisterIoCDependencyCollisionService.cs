using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyCollisionService : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.CollisionService",
                (object[] args) =>
                {
                    var grid = (SpatialPartitionGrid)args[0];
                    return new CollisionService();
                }
            )
            .Execute();
    }
}
