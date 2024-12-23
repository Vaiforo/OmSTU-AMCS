using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Commands.CommandInjectable",
                (object[] args) => new CommandInjectableCommand()
            )
            .Execute();
    }
}
