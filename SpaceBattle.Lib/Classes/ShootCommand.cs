using Hwdtech;

namespace SpaceBattle.Lib;

public class WeaponParameters
{
    public Vector SpawnPosition { get; }
    public Vector Direction { get; }
    public double ProjectileSpeed { get; }

    public WeaponParameters(Vector spawnPosition, Vector direction, double projectileSpeed)
    {
        SpawnPosition = spawnPosition;
        Direction = direction;
        ProjectileSpeed = projectileSpeed;
    }
}

public class ShootCommand : ICommand
{
    private readonly WeaponParameters parameters;

    public ShootCommand(WeaponParameters parameters)
    {
        this.parameters = parameters;
    }

    public void Execute()
    {
        var weaponObject = IoC.Resolve<IWeapon>("Weapon.Create");

        weaponObject.Setup(parameters);
        IoC.Resolve<ICommand>("Weapon.Setup", weaponObject).Execute();

        IoC.Resolve<ICommand>("Game.Item.Add", weaponObject).Execute();
    }
}
