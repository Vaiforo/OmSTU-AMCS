using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class AngleTest
{
    [Fact]
    public void ObjectInitPositiveTest()
    {
        Angle a = new Angle(1, 3);
        int degrees = 1;
        int sectors = 3;
        Assert.True(degrees.Equals(1));
        Assert.True(sectors.Equals(3));
    }

    [Fact]
    public void ObjectInitNegativeTest()
    {
        Assert.Throws<ArgumentException>(() => new Angle(5, 3));
    }



    [Fact]
    public void AnglesEqualPositiveTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(1, 3);
        Assert.True(a.Equals(b));
    }

    [Fact]
    public void AnglesEqualNegativeTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(1, 4);
        Assert.False(a.Equals(b));
    }

    
    [Fact]
    public void AttemptingToAddValueAnglePoditiveTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(-2, 3);
        Angle res = a + b;
        Angle expA = new Angle(-1, 3);
        Assert.True(res.Equals(expA));
    }

    [Fact]
    public void AttemptingToAddValueAngleNegativeTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(-2, 3);
        Angle res = a + b;
        Angle unexpA = new Angle(-2, 3);
        Assert.False(res.Equals(unexpA));
    }

    [Fact]
    public void AttemptingToOperandPositiveTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(-2, 3);
        Angle res = a + b;
        Angle unexpA = new Angle(-1, 3);
        Assert.True(res == unexpA);
    }
    [Fact]
    public void AttemptingToOperandNegativeTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(-2, 3);
        Angle res = a + b;
        Angle unexpA = new Angle(-2, 3);
        Assert.False(res == unexpA);
    }
    [Fact]
    public void AttemptingToNegativeOperandNegativeTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(-2, 3);
        Angle res = a + b;
        Angle unexpA = new Angle(-1, 3);
        Assert.False(res != unexpA);
    }
    [Fact]
    public void AttemptingToNegativeOperandPositiveTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(-2, 3);
        Angle res = a + b;
        Angle unexpA = new Angle(-2, 3);
        Assert.True(res != unexpA);
    }

    [Fact]
    public void AttemptingToGetHashCodePositiveTest()
    {
        Angle a = new Angle(1, 3);
        Angle b = new Angle(1, 3);
        Assert.True(a.GetHashCode == a.GetHashCode);
    }

        [Fact]
    public void AttemptingToGetHashCodeNegativeTest()
    {
        Angle a = new Angle(1, 3);
        Assert.False(a.GetHashCode == new Angle(1, 3).GetHashCode);
    }
}