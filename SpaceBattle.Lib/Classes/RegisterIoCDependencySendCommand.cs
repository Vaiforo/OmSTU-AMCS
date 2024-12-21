using App;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Send",
            (object[] args) =>
            {
                var cmd = (ICommand)args[0];
                var receiver = (ICommandReciever)args[1];
                return new SendCommand(cmd, receiver);
            }
        );
    }
}
