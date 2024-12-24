using Hwdtech;

namespace SpaceBattle.Lib;

public class EndCommand : ICommand
{
    private readonly IDictionary<string, object> _dict;
    private readonly string _label;
    private readonly RegisterIoCDependencyEmptyCommand registerIoCDependencyEmptyCommand =
        new RegisterIoCDependencyEmptyCommand();

    public EndCommand(IDictionary<string, object> dict, string label)
    {
        _dict = dict;
        _label = label;
        registerIoCDependencyEmptyCommand.Execute();
    }

    public void Execute()
    {
        var commandInjectable = (ICommandInjectable)_dict[_label];
        commandInjectable.Inject(IoC.Resolve<EmptyCommand>("Commands.Empty"));
    }
}
