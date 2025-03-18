using Hwdtech;

namespace SpaceBattle.Lib;

public class MacroCommand : ICommand
{
    private readonly IEnumerable<ICommand> _commands;

    public MacroCommand(IEnumerable<ICommand> commands)
    {
        _commands = commands;
    }

    public void Execute()
    {
        _commands.ToList().ForEach(cmd => cmd.Execute());
    }
}
