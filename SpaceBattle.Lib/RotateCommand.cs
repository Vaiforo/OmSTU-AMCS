namespace SpaceBattle.Lib;
public class TurnCommand : ICommand
{
    private IRotatingObject rotatingObject;
    public TurnCommand(IRotatingObject turnable)
    {
        rotatingObject = turnable;
    }
    public void Execute()
    {
        rotatingObject.Angle += rotatingObject.AngleVelocity;
    }
}