namespace SpaceBattle.Lib;

public class StartMoving : ICommand
{
    private readonly ICommand _command;
    private readonly IDictionary<string, object> _dict;
    private readonly ISender _queue;


    public StartMoving(ICommand command, IDictionary<string, object> dict, ISender queue)
    {
        _command = command;
        _dict = dict;
        _queue = queue;
    }

    public void Execute() {
        _dict["moveCommand"] = _command;
        _queue.Add(_command);
    }
}
