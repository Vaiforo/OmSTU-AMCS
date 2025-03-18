namespace SpaceBattle.Lib;

public interface IRotatingObject
{
    Angle Angle { get; set; }
    Angle AngleVelocity { get; }
}
