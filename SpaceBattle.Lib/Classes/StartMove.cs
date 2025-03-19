using Hwdtech;

namespace SpaceBattle.Lib;

public class StartMove : ICommand
{
    private readonly IMovingObject _gameObject;
    private readonly IDictionary<string, object> _dict;
    private readonly ISender _queue;
    private readonly string _label;

    public StartMove(
        IMovingObject gameObject,
        IDictionary<string, object> dict,
        ISender queue,
        string label
    )
    {
        _gameObject = gameObject;
        _dict = dict;
        _queue = queue;
        _label = label;
    }

    public void Execute()
    {
        var command = IoC.Resolve<ICommand>("Commands.Move", _gameObject);

        Dictionary<string, object> context = new()
        {
            { "Command", command },
            { "Dictionary", _dict },
            { "Sender", _queue },
            { "Label", _label },
        };

        IoC.Resolve<ICommand>("Actions.Start", context).Execute();
    }
}
