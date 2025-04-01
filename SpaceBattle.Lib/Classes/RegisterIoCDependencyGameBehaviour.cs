using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameBehaviour : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register",
        "Game.GameBehaviour",

        (object[] args) => {
            var command = IoC.Resolve<ICommand>("Game.Queue.Get");

            try
            {
                command.Execute();
            }
            catch (Exception e)
            {
                IoC.Resolve<ICommand>("ExceptionHandler.Handle", e).Execute();
            }

            return new EmptyCommand();
        }

        ).Execute();
    }
}