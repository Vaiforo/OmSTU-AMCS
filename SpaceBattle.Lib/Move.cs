interface IMovingObject
{
    Vector Location { get; set; }
    Vector Velocity { get; }
}

class Move(IMovingObject movingObject) : ICommand
{
    private IMovingObject movingObject = movingObject;

    public void Execute()
    {
        movingObject.Location += movingObject.Velocity;
    }
}

public class Vector(int[] coords)
{
    int[] Coords = coords;

    public static Vector operator +(Vector v1, Vector v2)
    {
        Vector result = new(v1.Coords.Zip(v2.Coords, (x, y) => x + y).ToArray());
        return result;

    }
}