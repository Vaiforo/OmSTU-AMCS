using Hwdtech;

namespace SpaceBattle.Lib;

public class TorpedoStartMove : ICommand
{
    private readonly IMovingObject _torpedo;
    private readonly IDictionary<string, object> _dict;
    private readonly ISender _queue;

    public TorpedoStartMove(IMovingObject torpedo, IDictionary<string, object> dict, ISender queue)
    {
        _torpedo = torpedo;
        _dict = dict;
        _queue = queue;
    }

    public void Execute()
    {
        var command = IoC.Resolve<ICommand>("Commands.Move", _torpedo);

        Dictionary<string, object> context = new()
        {
            { "Command", command },
            { "Dictionary", _dict },
            { "Sender", _queue },
            { "Label", "MoveTorpedo" },
        };

        IoC.Resolve<ICommand>("Actions.Start", context).Execute();
    }
}
