using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyCollisionCheckCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.CollisionCheckCommand",
                (object[] args) =>
                {
                    var obj = (IMovingObject)args[0];
                    return new CollisionCheckCommand(obj);
                }
            )
            .Execute();
    }
}
