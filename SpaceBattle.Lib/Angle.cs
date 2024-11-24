namespace SpaceBattle.Lib;

public class Angle
{
    /*public int degrees { get; set; }

    public Angle(int degrees)
    {
        this.degrees = (degrees % 360 + 360) % 360;
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        return new Angle(a1.degrees + a2.degrees);
    }*/

    public int sector { get; set; }
    public int separation { get; set; }

    public Angle(int sector, int separation)
    {
        this.sector = sector;
        this.separation = separation;
    }

    public static Angle operator +(Angle u1, Angle u2)
    {
        if (u1.separation == u2.separation)
        {
            return new Angle((u1.sector + u2.sector) % u1.separation, u1.separation);
        }
        else
        {
            throw new Exception();
        }
    }
}