using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameCanContinue : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register",
        "Game.CanContinue",
        (object[] args) =>
        {
            var elapsedTime = (long)args[0];
            var allowedTime = IoC.Resolve<int>("Game.AllowedTime.Get");
            var queueCount = IoC.Resolve<int>("Game.Queue.Count");

            return (object)(elapsedTime < allowedTime && !(queueCount == 0));
        }
        ).Execute();
    }
}
