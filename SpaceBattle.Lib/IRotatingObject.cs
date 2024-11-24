namespace SpaceBattle.Lib;

public interface IRotatingObject
{
    public Angle Angle { get; set; }
    public Angle AngleVelocity { get; set; }
}