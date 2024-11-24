namespace SpaceBattle.Lib;

public class RotateCommand : ICommand
{
    private readonly IRotatingObject rotatingObject;

    public RotateCommand(IRotatingObject rotatingObject)
    {
        this.rotatingObject = rotatingObject;
    }

    public void Execute()
    {
        rotatingObject.Angle += rotatingObject.AngleVelocity;
        //rotatingObject.Angle.degrees = Angle.summ(rotatingObject.Angle, rotatingObject.AngleVelocity).degrees;
    }
}