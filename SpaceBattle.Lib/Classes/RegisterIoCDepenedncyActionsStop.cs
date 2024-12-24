using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Actions.Stop",
                (object[] args) =>
                {
                    var order = (IDictionary<string, object>)args[0];
                    var dict = (IDictionary<string, object>)order["Dictionary"];
                    var label = (string)order["Label"];
                    return new EndCommand(dict, label);
                }
            )
            .Execute();
    }
}
