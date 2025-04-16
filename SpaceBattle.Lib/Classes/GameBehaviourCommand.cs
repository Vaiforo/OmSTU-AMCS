using Hwdtech;

namespace SpaceBattle.Lib;

public class GameBehaviourCommand : ICommand
{
    public void Execute()
    {
        var command = IoC.Resolve<ICommand>("Game.Queue.Get");

        try
        {
            command.Execute();
        }
        catch (Exception e)
        {
            IoC.Resolve<ICommand>("ExceptionHandler.Handle", command, e).Execute();
        }
    }
}