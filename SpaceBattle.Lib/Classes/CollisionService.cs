﻿using Hwdtech;

namespace SpaceBattle.Lib;

public class CollisionService : ICommand
{
    public void Execute()
    {
        var grid = (SpatialPartitionGrid)IoC.Resolve<object>("Game.SpatialGrid");

        var commands = grid.GetAllObjects()
            .Select(obj => IoC.Resolve<ICommand>("Commands.CollisionCheckCommand", obj))
            .ToList();

        IoC.Resolve<ICommand>("Commands.Macro", commands).Execute();
    }
}
