using App;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Commands.Move",
                (object[] args) =>
                    new MoveCommand(Ioc.Resolve<IMovingObject>("Adapters.IMovingObject", args[0]))
            )
            .Execute();
    }
}
