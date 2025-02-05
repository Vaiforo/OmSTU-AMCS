using Hwdtech;

namespace SpaceBattle.Lib;

public class StartCommand : ICommand
{
    private readonly ICommand _command;
    private readonly IDictionary<string, object> _dict;
    private readonly ISender _queue;
    private readonly string _label;

    public StartCommand(
        ICommand command,
        IDictionary<string, object> dict,
        ISender queue,
        string label
    )
    {
        _command = command;
        _dict = dict;
        _queue = queue;
        _label = label;
    }

    public void Execute()
    {
        _dict[_label] = _command;
        _queue.Add(_command);
    }
}
