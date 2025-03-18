using System.Collections;
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
    }
}