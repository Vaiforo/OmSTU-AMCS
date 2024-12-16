namespace SpaceBattle.Lib;
public class TurnCommand : ICommand
{
    private readonly IRotatingObject rotatingObject;
    public TurnCommand(IRotatingObject turnable)
    {
        rotatingObject = turnable;
    }
    public void Execute()
    {
        rotatingObject.Angle += rotatingObject.AngleVelocity;
    }
}