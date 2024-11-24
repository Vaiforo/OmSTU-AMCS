namespace SpaceBattle.Lib;
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