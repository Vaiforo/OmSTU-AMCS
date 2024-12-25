namespace SpaceBattle.Lib;

public class RotateCommand
{
    private readonly IRotatingObject rotatingObject;

    public RotateCommand(IRotatingObject turnable)
    {
        rotatingObject = turnable;
    }

    public void Execute()
    {
        rotatingObject.Angle += rotatingObject.AngleVelocity;
    }
}
