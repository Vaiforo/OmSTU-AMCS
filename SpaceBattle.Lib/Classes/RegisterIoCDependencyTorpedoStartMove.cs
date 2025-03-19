using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyTorpedoStartMove : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Actions.MoveTorpedo",
                (object[] args) =>
                {
                    var order = (IDictionary<string, object>)args[0];
                    var torpedo = (IMovingObject)order["Torpedo"];
                    var dict = (IDictionary<string, object>)order["Dictionary"];
                    var queue = (ISender)order["Sender"];
                    return new TorpedoStartMove(torpedo, dict, queue);
                }
            )
            .Execute();
    }
}
