using SpaceBattle.Lib;

public interface IWeapon
{
    Vector SpawnPosition { get; }
    Vector Direction { get; }
    double ProjectileSpeed { get; }
}
