using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyAuthCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.AuthCommand",
                (object[] args) =>
                {
                    var order = (IDictionary<string, object>)args[0];
                    var userID = (string)order["UserID"];
                    var objectID = (string)order["ObjectID"];
                    var action = (string)order["Action"];
                    return new AuthCommand(userID, objectID, action);
                }
            )
            .Execute();
    }
}
