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
            var allowedTime = IoC.Resolve<Func<int>>("Game.AllowedTime.Get")();
            var queueCount = IoC.Resolve<Func<int>>("Game.Queue.Count")();

            if (elapsedTime < allowedTime && !(queueCount == 0))
            {
                return () => true;
            }
            else
            {
                return () => false;
            }
        }

        ).Execute();
    }
}
