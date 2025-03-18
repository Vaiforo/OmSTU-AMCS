using Hwdtech;

namespace SpaceBattle.Lib;

public class TorpedoStartMove : ICommand
{
    private readonly IMovingObject _torpedo;
    private readonly IDictionary<string, object> _dict;
    private readonly ISender _sender;

    public TorpedoStartMove(IMovingObject torpedo, IDictionary<string, object> dict, ISender sender)
    {
        _torpedo = torpedo;
        _dict = dict;
        _sender = sender;
    }

    public void Execute()
    {
        var command = IoC.Resolve<ICommand>("Commands.Move", _torpedo);

        Dictionary<string, object> context = new()
        {
            { "Command", command },
            { "Dictionary", _dict },
            { "Sender", _sender },
            { "Label", "MoveTorpedo" },
        };

        IoC.Resolve<ICommand>("Actions.Start", context).Execute();
    }
}
