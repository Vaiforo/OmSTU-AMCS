namespace SpaceBattle.Lib;
public class RotateCommand : ICommand
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
