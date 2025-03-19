using System.Diagnostics;
using Hwdtech;

namespace SpaceBattle.Lib;

public class Game : ICommand
{
    private readonly object _gameScope;

    public Game(object gameScope)
    {
        _gameScope = gameScope;
    }

    public void Execute()
    {
        var stopwatch = Stopwatch.StartNew();

        IoC.Resolve<ICommand>("Scopes.Current.Set", _gameScope).Execute();
        var queue = IoC.Resolve<IQueue>("Game.Queue");

        while(stopwatch.ElapsedMilliseconds < 50 && queue.Count() == 0)
        {
            var cmd = queue.Take();
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        stopwatch.Stop();
    }
}