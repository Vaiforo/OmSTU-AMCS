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
        IoC.Resolve<ICommand>("Scopes.Current.Set", _gameScope).Execute();

        var gameGivenTime = IoC.Resolve<int>("Game.GivenTime.Get");
        var stopwatch = Stopwatch.StartNew();

        while(stopwatch.ElapsedMilliseconds < gameGivenTime && !IoC.Resolve<bool>("Game.Queue.IsEmpty"))
        {
            var cmd = IoC.Resolve<ICommand>("Game.Queue.Take");
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