using Hwdtech;
namespace SpaceBattle.Lib;

public class CreateMacroCommandStrategy(string commandSpec)
{
    private readonly string _commandSpec = commandSpec;

    public ICommand Resolve(object[] args)
    {
        var commandNames = IoC.Resolve<List<string>>("Specs." + _commandSpec);
        var commands = commandNames.Select(n => IoC.Resolve<ICommand>(n, args)).ToArray();
        var macroCommand = IoC.Resolve<ICommand>("Commands.Macro", commands.Cast<object>().ToArray());

        return macroCommand;
    }
}
