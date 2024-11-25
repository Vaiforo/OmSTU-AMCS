using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class VectorTests
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
    public void DataEqualVectorsPositiveTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [1, 2, 3];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.True(vector1.GetCoords().SequenceEqual(vector2.GetCoords()));
    }

    [Fact]
    public void DataEqualVectorsDiffLenNegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [1, 2];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.False(vector1.GetCoords().SequenceEqual(vector2.GetCoords()));
    }

    [Fact]
    public void DataEqualVectorsEquLenNegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [4, 4, 4];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.False(vector1.GetCoords().SequenceEqual(vector2.GetCoords()));
    }

    [Fact]
    public void ObjectEqualVectorsPositiveTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [1, 2, 3];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.True(vector1.Equals(vector2));
    }

    [Fact]
    public void ObjectEqualVectorsNegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        int[] coords2 = [5, 5, 5];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.False(vector1.Equals(vector2));
    }

    [Fact]
    public void ObjectCopyVectorsPositiveTest()
    {
        int[] coords1 = [1, 2, 3];
        var vector1 = new Vector(coords1);
        var vector2 = vector1;
        Assert.True(vector1.Equals(vector2));
    }

    [Fact]
    public void ObjectEqualNullNegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        var vector1 = new Vector(coords1);
        Assert.False(vector1.Equals(null));
    }

    [Fact]
    public void ObjectEqualNotVectorNegativeTest()
    {
        int[] coords1 = [1, 2, 3];
        var vector1 = new Vector(coords1);
        object notVector = "not a vector";
        Assert.False(vector1.Equals(notVector));
    }

    [Fact]
    public void OperatorAddPositiveTest()
    {
        int[] coords1 = [1, 2];
        int[] coords2 = [2, 2];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        var resultVector = vector1 + vector2;
        var expectedVector = new Vector([3, 4]);
        Assert.True(resultVector.Equals(expectedVector));
    }

    [Fact]
    public void OperatorAddDiffLenNegativeTest()
    {
        int[] coords1 = [1, 2];
        int[] coords2 = [2, 2, 2];
        var vector1 = new Vector(coords1);
        var vector2 = new Vector(coords2);
        Assert.Throws<InvalidDataException>(() => vector1 + vector2);
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
