﻿using System.Diagnostics;
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

        while (IoC.Resolve<bool>("Game.CanContinue", stopwatch.ElapsedMilliseconds))
        {
            IoC.Resolve<Action>("Game.GameBehaviour")();
        }

        stopwatch.Stop();
    }
}
