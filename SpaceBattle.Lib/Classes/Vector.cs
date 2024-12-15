namespace SpaceBattle.Lib;

public class Vector
{
    private int[] _coords;

    public Vector(params int[] coords)
    {
        _coords = coords;
    }

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        if (vector1._coords.Length != vector2._coords.Length)
        {
            throw new ArgumentException("Vectors must have the same length");
        }

        var result = new Vector(new int[vector1._coords.Length])
        {
            _coords = vector1
                ._coords.Select((value, index) => value + vector2._coords[index])
                .ToArray(),
        };
        return result;
    }

    public static bool operator ==(Vector vector1, Vector vector2)
    {
        return vector1._coords.SequenceEqual(vector2._coords);
    }

    public static bool operator !=(Vector vector1, Vector vector2)
    {
        return !(vector1 == vector2);
    }

    public override bool Equals(object? obj)
    {
        return obj != null && obj is Vector vector && _coords.SequenceEqual(vector._coords);
    }

    public override int GetHashCode()
    {
        return _coords.GetHashCode();
    }

    public int[] GetCoords()
    {
        return _coords;
    }
}
