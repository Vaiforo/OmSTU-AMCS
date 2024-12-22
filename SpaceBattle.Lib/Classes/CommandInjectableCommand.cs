namespace SpaceBattle.Lib;

public class CommandInjectableCommand : ICommand, ICommandInjectable
{
    public ICommand? _command;

    public void Execute()
    {
        _command!.Execute();
    }

    public void Inject(ICommand cmd)
    {
        _command = cmd;
    }
}
