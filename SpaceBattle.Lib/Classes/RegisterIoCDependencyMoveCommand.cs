using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : Hwdtech.ICommand
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
