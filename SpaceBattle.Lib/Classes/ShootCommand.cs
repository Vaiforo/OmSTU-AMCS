using Hwdtech;

namespace SpaceBattle.Lib;

public class ShootCommand : ICommand
{
    private readonly Vector position;
    private readonly Vector shootDirection;
    private readonly double speed;

    public ShootCommand(Vector position, Vector shootDirection, double speed)
    {
        this.position = position;
        this.shootDirection = shootDirection;
        this.speed = speed;
    }

    public void Execute()
    {
        var weaponId = Guid.NewGuid().ToString();
        var weaponDict = IoC.Resolve<IDictionary<string, object>>("Weapon.Create", weaponId);

        var weapon = IoC.Resolve<IMovingObject>("Adapters.IMovingObject", weaponDict["Id"]);

        IoC.Resolve<ICommand>("Weapon.Setup", weapon, position, shootDirection, speed).Execute();

        IoC.Resolve<ICommand>("GameItem.Add", weaponId, weapon).Execute();
    }
}
