using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Macro",
                (object[] args) =>
                    (ICommand)new MacroCommand(args.Select(command => (ICommand)command).ToArray())
            )
            .Execute();
    }
}
