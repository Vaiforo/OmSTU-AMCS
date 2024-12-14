using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class AngleTest
{
    [Fact]
    public void ObjectInitPositiveTest()
    {
        var a = new Angle(1, 3);
        var degrees = 1;
        var sectors = 3;
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
        var a = new Angle(15, 8);
        var b = new Angle(23, 8);
        Assert.True(a.Equals(b));
    }

        [Fact]
    public void AnglesOperatorEqualPositiveTest()
    {
        var a = new Angle(15, 8);
        var b = new Angle(23, 8);
        Assert.True(a == b);
    }

    [Fact]
    public void AnglesEqualNegativeTest()
    {
        var a = new Angle(1, 8);
        var b = new Angle(2, 8);
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void AnglesOperatirNotEqualPositiveTest()
    {
        var a = new Angle(1, 8);
        var b = new Angle(2, 8);
        Assert.True(a != b);
    }

    
    [Fact]
    public void AttemptingToAddValueAnglePoditiveTest()
    {
        var a = new Angle(5, 8);
        var b = new Angle(7, 8);
        var res = a + b;
        var expA = new Angle(4, 8);
        Assert.True(res.Equals(expA));
    }

    [Fact]
    public void AttemptingToAddValueAngleNegativeTest()
    {
        var a = new Angle(1, 3);
        var b = new Angle(-2, 3);
        var res = a + b;
        var unexpA = new Angle(-2, 3);
        Assert.False(res.Equals(unexpA));
    }

    [Fact]
    public void AttemptingToOperandPositiveTest()
    {
        var a = new Angle(1, 3);
        var b = new Angle(-2, 3);
        var res = a + b;
        var unexpA = new Angle(-1, 3);
        Assert.True(res == unexpA);
    }
    [Fact]
    public void AttemptingToOperandNegativeTest()
    {
        var a = new Angle(1, 3);
        var b = new Angle(-2, 3);
        var res = a + b;
        var unexpA = new Angle(-2, 3);
        Assert.False(res == unexpA);
    }
    [Fact]
    public void AttemptingToNegativeOperandNegativeTest()
    {
        var a = new Angle(1, 3);
        var b = new Angle(-2, 3);
        var res = a + b;
        var unexpA = new Angle(-1, 3);
        Assert.False(res != unexpA);
    }
    [Fact]
    public void AttemptingToNegativeOperandPositiveTest()
    {
        var a = new Angle(1, 3);
        var b = new Angle(-2, 3);
        var res = a + b;
        var unexpA = new Angle(-2, 3);
        Assert.True(res != unexpA);
    }

    [Fact]
    public void AttemptingToGetHashCodePositiveTest()
    {
        var a = new Angle(1, 3);
        var b = new Angle(1, 3);
        Assert.True(a.GetHashCode == a.GetHashCode);
    }

        [Fact]
    public void AttemptingToGetHashCodeNegativeTest()
    {
        var a = new Angle(1, 3);
        Assert.False(a.GetHashCode == new Angle(1, 3).GetHashCode);
    }
}