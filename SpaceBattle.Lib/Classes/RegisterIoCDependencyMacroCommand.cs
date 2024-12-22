using Hwdtech;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Macro", (object[] args) => new MacroCommand(
            IoC.Resolve<IEnumerable<ICommand>>("Adapters.IEnumerable<ICommand>", args[0])
        )).Execute();
    }
}