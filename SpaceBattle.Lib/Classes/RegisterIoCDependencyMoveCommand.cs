using App;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Move",
                (object[] args) =>
                    new MoveCommand((IMovingObject)((IDictionary<string, object>)args[0])["object"])
            )
            .Execute();
    }
}
