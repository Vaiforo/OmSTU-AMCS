namespace SpaceBattle.Lib;
/*public class Angle
{
    public int valAng { get; set; }
    public int sepAng { get; }
    public Angle(int valAng, int sepAng)
    {
        this.valAng = valAng;
        this.sepAng = sepAng;
    }
    public static Angle operator +(Angle a1, Angle a2)
    {
        if (a1.sepAng == a2.sepAng)
        {
            return new Angle((a1.valAng + a2.valAng) % a1.sepAng, a1.sepAng);
        }
        else
        {
            throw new Exception();
        }
    }
}*/

public class Angle
{
    public int degrees { get; set; }

    public Angle(int degrees)
    {
        this.degrees = (degrees % 360 + 360) % 360;
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        return new Angle(a1.degrees + a2.degrees);
    }
}