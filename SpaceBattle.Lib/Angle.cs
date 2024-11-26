namespace SpaceBattle.Lib;
public class Angle
{
    int degrees;
    int  sectors;

    public Angle(int d, int n) 
    {
        if(d >= n)
        {
            throw new ArgumentException("d must be less than n");
        }
        degrees = d;
        sectors = n;
    }

    public static Angle operator+(Angle a1, Angle a2)
    {
        if(a1.sectors != a2.sectors)
        {
            throw new ArgumentException("Operands Angle must have same division");
        }

        return new Angle((a1.degrees + a2.degrees) % a1.sectors, a1.sectors);
    }

    public static bool operator==(Angle a1, Angle a2)
    {
        return a1.Equals(a2);
    }

    public static bool operator!=(Angle a1, Angle a2)
    {
        return !(a1 == a2);
    }    

    public override bool Equals(object? obj)
    {
        if(obj is Angle)
        {
            Angle other = (Angle)obj;
            return degrees == other.degrees && sectors == other.sectors;
        }
        else
        { 
            return false; 
        }
    }

    public override int GetHashCode()
    {
        return ( ((double)degrees) / sectors * 360 ).GetHashCode();
    }
}
