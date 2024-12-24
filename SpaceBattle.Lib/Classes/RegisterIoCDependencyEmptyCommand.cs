using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyEmptyCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Commands.Empty",
                (object[] args) => new EmptyCommand()
            )
            .Execute();
    }
}
