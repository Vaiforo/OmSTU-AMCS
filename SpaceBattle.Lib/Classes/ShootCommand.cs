using Hwdtech;

namespace SpaceBattle.Lib;

public class ShootCommand : ICommand
{
    private readonly Vector spawnPosition;
    private readonly Vector direction;
    private readonly double projectileSpeed;

    public ShootCommand(Vector spawnPosition, Vector direction, double projectileSpeed)
    {
        this.spawnPosition = spawnPosition;
        this.direction = direction;
        this.projectileSpeed = projectileSpeed;
    }

    public void Execute()
    {
        var weaponGuid = Guid.NewGuid().ToString();
        var weaponCreationParams = IoC.Resolve<IDictionary<string, object>>(
            "Weapon.Create",
            weaponGuid
        );

        var weaponObject = IoC.Resolve<IMovingObject>(
            "Adapters.IMovingObject",
            weaponCreationParams["Id"]
        );

        IoC.Resolve<ICommand>(
                "Weapon.Setup",
                weaponObject,
                spawnPosition,
                direction,
                projectileSpeed
            )
            .Execute();

        IoC.Resolve<ICommand>("GameItem.Add", weaponGuid, weaponObject).Execute();
    }
}
