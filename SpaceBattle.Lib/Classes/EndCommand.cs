namespace SpaceBattle.Lib;

public class EndCommand : ICommand
{
    private readonly IDictionary<string, object> _dict;
    private readonly string _label;

    public EndCommand(IDictionary<string, object> dict, string label)
    {
        _dict = dict;
        _label = label;
    }

    public void Execute()
    {
        var commandInjectable = (CommandInjectableCommand)_dict[_label];
        commandInjectable.Inject(new EmptyCommand());
    }
}
