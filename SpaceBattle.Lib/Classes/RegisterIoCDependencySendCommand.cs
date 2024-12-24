using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Send",
                (object[] args) =>
                {
                    var command = (ICommand)args[0];
                    var reciever = (ICommandReciever)args[1];
                    return new SendCommand(command, reciever);
                }
            )
            .Execute();
    }
}
