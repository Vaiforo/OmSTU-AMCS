﻿using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyEmptyCommand : ICommand
{
    public void Execute()
    {
        var emptyCommand = new EmptyCommand();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.Empty", (object[] args) => emptyCommand)
            .Execute();
    }
}
