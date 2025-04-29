﻿using Hwdtech;

namespace SpaceBattle.Lib;

public class ShootCommand : ICommand
{
    private readonly IWeapon _weaponParams;

    public ShootCommand(IWeapon obj)
    {
        _weaponParams = obj;
    }

    public void Execute()
    {
        var weaponObject = IoC.Resolve<IWeapon>("Weapon.Create");

        IoC.Resolve<ICommand>("Weapon.Setup", weaponObject, _weaponParams).Execute();

        IoC.Resolve<ICommand>("Actions.Start", weaponObject).Execute();
    }
}
