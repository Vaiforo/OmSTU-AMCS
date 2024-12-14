using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Move",
                (object[] args) =>
                    new MoveCommand((IMovingObject)((IDictionary<string, object>)args[0])["object"])
            )
            .Execute();
    }
}
