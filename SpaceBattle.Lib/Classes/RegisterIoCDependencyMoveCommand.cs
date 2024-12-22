using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Commands.Move",
                (object[] args) =>
                    new MoveCommand(IoC.Resolve<IMovingObject>("Adapters.IMovingObject", args[0]))
            )
            .Execute();
    }
}
