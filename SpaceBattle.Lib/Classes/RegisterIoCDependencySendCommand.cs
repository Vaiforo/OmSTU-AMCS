using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Commands.Send",
                (object[] args) =>
                    new SendCommand(
                        IoC.Resolve<ICommand>("Adapters.ICommand", args[0]),
                        IoC.Resolve<ICommandReciever>("Adapters.ICommandReciever", args[1])
                    )
            )
            .Execute();
    }
}
