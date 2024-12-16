namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _movingObject;

    public MoveCommand(IMovingObject movingObject)
    {
        _movingObject = movingObject;
    }

    public void Execute()
    {
        _movingObject.Position += _movingObject.Velocity;
    }
}
