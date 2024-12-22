using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Action.Stop",
                (object[] args) => new EndCommand()
            )
            .Execute();
    }
}
