namespace SpaceBattle.Lib;

public class StartCommad : ICommand
{
    private ICommand _command;
    private IDictionary<string, object> _dict;
    private ISender _queue;

    public StartCommad(ICommand command, IDictionary<string, object> dict, ISender queue)
    {
        _command = command;
        _dict = dict;
        _queue = queue;
    }

    public void Execute()
    {
        _dict["startCommand"] = _command;
        _queue.Add(_command);
    }
}
