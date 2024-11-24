namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private readonly IMovingObject movingObject;

    public MoveCommand(IMovingObject movingObject)
    {
        this.movingObject = movingObject;
    }

    public void Execute()
    {
        movingObject.Position += movingObject.Velocity;
    }
}
