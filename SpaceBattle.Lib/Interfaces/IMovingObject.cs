namespace SpaceBattle.Lib;

public interface IMovingObject
{
    Vector Position { get; set; }
    Vector Velocity { get; }
}
