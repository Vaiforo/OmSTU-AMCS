using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyStartMove : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Actions.StartMove",
                (object[] args) =>
                {
                    var order = (IDictionary<string, object>)args[0];
                    var torpedo = (IMovingObject)order["GameObject"];
                    var dict = (IDictionary<string, object>)order["Dictionary"];
                    var queue = (ISender)order["Sender"];
                    var label = (string)order["Label"];
                    return new StartMove(torpedo, dict, queue, label);
                }
            )
            .Execute();
    }
}
