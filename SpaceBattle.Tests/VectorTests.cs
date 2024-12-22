using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class VectorTestsNew
{
    [Fact]
    public void ObjectInitVector2DPositiveTest()
    {
        int[] coords = [1, 2];
        var vector = new Vector(coords);
        Assert.True(coords.SequenceEqual(vector.GetCoords()));
    }

    [Fact]
    public void ObjectInitVector3DPositiveTest()
    {
        int[] coords = [1, 2, 3];
        var vector = new Vector(coords);
        Assert.True(coords.SequenceEqual(vector.GetCoords()));
    }

    [Fact]
    public void OperatorAddPositiveTest()
    {
        int[] coords1 = [1, -1, 2];
        int[] coords2 = [-1, 1, -2];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        var resultVector = vector1 + vector2;
        var expectedVector = new Vector([0, 0, 0]);
        Assert.True(resultVector.Equals(expectedVector));
    }

    [Fact]
    public void OperatorAddDiffLen1NegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [2, 2];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void OperatorAddDiffLen2NegativeTest()
    {
        int[] coords1 = [1, 2];
        int[] coords2 = [2, 2, 3];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void ObjectEqualsVectorsPositiveTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [1, 2, 3];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.True(vector1.Equals(vector2));
    }

    [Fact]
    public void ObjectEqualsVectorsNegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [3, 2, 1];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.False(vector1.Equals(vector2));
    }

    [Fact]
    public void ObjectEqualVectorsPositiveTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [1, 2, 3];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.True(vector1 == vector2);
    }

    [Fact]
    public void ObjectEqualVectorsNegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [3, 2, 1];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.False(vector1 == vector2);
    }

    [Fact]
    public void ObjectNotEqualVectorsPositiveTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [3, 2, 1];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.True(vector1 != vector2);
    }

    [Fact]
    public void ObjectNotEqualVectorsNegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [1, 2, 3];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.False(vector1 != vector2);
    }

    [Fact]
    public void FuncHashCodePossitiveTest()
    {
        int[] coords1 = [1, 2];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords1);
        Assert.Equal(vector1.GetHashCode(), vector2.GetHashCode());
    }

    [Fact]
    public void FuncHashCodeTest()
    {
        var vector1 = new Vector(1, 2);
        var vector2 = new Vector(1, 2);
        Assert.NotEqual(vector1.GetHashCode(), vector2.GetHashCode());
    }

    [Fact]
    public void FuncHashCodeNegativeTest()
    {
        int[] coords1 = [1, 2];
        int[] coords2 = [1, 2];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.NotEqual(vector1.GetHashCode(), vector2.GetHashCode());
    }
}
