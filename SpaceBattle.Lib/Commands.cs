using System.Windows.Input;


interface ICommand
{
    void Execute();
}


class MacroCommand(ICommand[] commands) : ICommand
{
    private ICommand[] commands = commands;

    public void Execute()
    {
        Array.ForEach(commands, cmd => cmd.Execute());
    }
}