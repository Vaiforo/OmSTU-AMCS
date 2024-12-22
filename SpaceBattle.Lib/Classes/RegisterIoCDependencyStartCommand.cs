using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Actions.Start",
                (object[] args) =>
                {
                    var order = (IDictionary<string, object>)args[0];
                    var cmd = (ICommand)order["Command"];
                    var dict = (IDictionary<string, object>)order["Dictionary"];
                    var queue = (ISender)order["Sender"];
                    var label = (string)order["Label"];
                    return new StartCommand(cmd, dict, queue, label);
                }
            )
            .Execute();
    }
}
