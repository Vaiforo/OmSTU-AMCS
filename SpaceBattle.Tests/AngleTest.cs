using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class AngleTest
{
[Fact]
    public void ConstructorShouldNormalizeDegreesWhenDegreesIsNegativeTest()
    {
        int degrees = -45;

        Angle angle = new Angle(degrees);

        Assert.Equal(315, angle.degrees);
    }

    [Fact]
    public void ConstructorShouldNormalizeDegreesWhenDegreesIsGreaterThan360Test()
    {
        int degrees = 450;

        Angle angle = new Angle(degrees);

        Assert.Equal(90, angle.degrees);
    }

    [Fact]
    public void AddShouldReturnCorrectSumWhenAddingTwoAnglesTest()
    {
        Angle angle1 = new Angle(30);
        Angle angle2 = new Angle(45);

        Angle result = angle1 + angle2;

        Assert.Equal(75, result.degrees); 
    }

    [Fact]
    public void AddShouldNormalizeSumWhenResultIsGreaterThan360Test()
    {
        Angle angle1 = new Angle(350);
        Angle angle2 = new Angle(20);

        Angle result = angle1 + angle2;

        Assert.Equal(10, result.degrees);
    }
}