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
                    new SendCommand(
                        Ioc.Resolve<ICommand>("Adapters.ICommand", args[0]),
                        Ioc.Resolve<ICommandReciever>("Adapters.ICommandReciever", args[1])
                    )
            )
            .Execute();
    }
}
