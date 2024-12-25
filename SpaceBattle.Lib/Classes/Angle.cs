namespace SpaceBattle.Lib;

public class Angle
{
    public int degrees { get; set; }
    public int sectors { get; }

    public Angle(int d, int n)
    {
        if (n == 0)
        {
            throw new DivideByZeroException();
        }

        degrees = d % n;
        sectors = n;
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        if (a1.sectors != a2.sectors)
        {
            throw new ArgumentException("Operands Angle must have same division");
        }

        return new Angle((a1.degrees + a2.degrees) % a1.sectors, a1.sectors);
    }

    public static bool operator ==(Angle a1, Angle a2)
    {
        return a1.Equals(a2);
    }

    public static bool operator !=(Angle a1, Angle a2)
    {
        return !(a1 == a2);
    }

    public double ToRadians()
    {
        double _degrees = degrees * 360 / sectors;
        return _degrees * Math.PI / 180.0;
    }

    public static implicit operator double(Angle a)
    {
        return (double)a.degrees / a.sectors * 2 * Math.PI;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Angle other)
        {
            return degrees % sectors == other.degrees % other.sectors && sectors == other.sectors;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return new { degrees, sectors }.GetHashCode();
    }
}
