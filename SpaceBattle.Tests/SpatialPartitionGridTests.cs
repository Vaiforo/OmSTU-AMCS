using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SpatialPartitionGridTests
{
    private class MockMovingObject(int[] coords) : IMovingObject
    {
        public Vector Position { get; set; } = new Vector(coords);
        public Vector Velocity { get; } = new Vector(0, 0);
    }

    [Fact]
    public void AddToGridOneObjPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj = new MockMovingObject([15, 25]);

        grid.AddToGrid(obj);

        var cell = grid.GetCell([15, 25]);
        var objectsInCell = grid.GetObjectsInCell(cell);
        Assert.Contains(obj, objectsInCell);
        Assert.Single(objectsInCell);
    }

    [Fact]
    public void AddToGridTwoObjPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj1 = new MockMovingObject([15, 25]);
        var obj2 = new MockMovingObject([16, 26]);

        grid.AddToGrid(obj1);
        grid.AddToGrid(obj2);

        var cell = grid.GetCell([15, 25]);
        var objectsInCell = grid.GetObjectsInCell(cell);
        Assert.Contains(obj1, objectsInCell);
        Assert.Contains(obj2, objectsInCell);
        Assert.Equal(2, objectsInCell.Count);
    }

    [Fact]
    public void RemoveFromGridPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj = new MockMovingObject([15, 25]);
        grid.AddToGrid(obj);

        grid.RemoveFromGrid(obj);

        var cell = grid.GetCell([15, 25]);
        var objectsInCell = grid.GetObjectsInCell(cell);
        Assert.Empty(objectsInCell);
    }

    [Fact]
    public void RemoveFromGridNegativeTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj = new MockMovingObject([15, 25]);

        grid.RemoveFromGrid(obj);

        var cell = grid.GetCell([15, 25]);
        var objectsInCell = grid.GetObjectsInCell(cell);
        Assert.Empty(objectsInCell);
    }

    [Fact]
    public void UpdatePositionNoChangePositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj = new MockMovingObject([15, 25]);
        grid.AddToGrid(obj);

        obj.Position = new Vector([16, 26]);
        grid.UpdatePosition(obj);

        var cell = grid.GetCell([15, 25]);
        var objectsInCell = grid.GetObjectsInCell(cell);
        Assert.Contains(obj, objectsInCell);
        Assert.Single(objectsInCell);
    }

    [Fact]
    public void UpdatePositionChangedPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj = new MockMovingObject([15, 25]);
        grid.AddToGrid(obj);

        obj.Position = new Vector([35, 45]);
        grid.UpdatePosition(obj);

        var oldCell = grid.GetCell([15, 25]);
        var newCell = grid.GetCell([35, 45]);
        Assert.Empty(grid.GetObjectsInCell(oldCell));
        Assert.Contains(obj, grid.GetObjectsInCell(newCell));
        Assert.Single(grid.GetObjectsInCell(newCell));
    }

    [Fact]
    public void UpdatePositionNegativeTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj = new MockMovingObject([15, 25]);

        grid.UpdatePosition(obj);

        var cell = grid.GetCell([15, 25]);
        Assert.Empty(grid.GetObjectsInCell(cell));
    }

    [Fact]
    public void GetNearbyPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj1 = new MockMovingObject([15, 25]);
        var obj2 = new MockMovingObject([16, 26]);
        grid.AddToGrid(obj1);
        grid.AddToGrid(obj2);

        var nearby = grid.GetNearby(obj1);

        Assert.Contains(obj2, nearby);
        Assert.DoesNotContain(obj1, nearby);
        Assert.Single(nearby);
    }

    [Fact]
    public void GetNearbyDifCellsPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj1 = new MockMovingObject([15, 25]);
        var obj2 = new MockMovingObject([25, 25]);
        grid.AddToGrid(obj1);
        grid.AddToGrid(obj2);

        var nearby = grid.GetNearby(obj1);

        Assert.Contains(obj2, nearby);
        Assert.DoesNotContain(obj1, nearby);
        Assert.Single(nearby);
    }

    [Fact]
    public void GetNearbyEmptyPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj = new MockMovingObject([15, 25]);

        var nearby = grid.GetNearby(obj);

        Assert.Empty(nearby);
    }

    [Fact]
    public void GetAllObjectsTwoObjPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj1 = new MockMovingObject([15, 25]);
        var obj2 = new MockMovingObject([35, 45]);
        grid.AddToGrid(obj1);
        grid.AddToGrid(obj2);

        var objects = grid.GetAllObjects().ToList();

        Assert.Contains(obj1, objects);
        Assert.Contains(obj2, objects);
        Assert.Equal(2, objects.Count);
    }

    [Fact]
    public void GetAllObjectsOneObjPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var obj = new MockMovingObject([15, 25]);
        grid.AddToGrid(obj);

        var cell = grid.GetCell([15, 25]);
        var objects = grid.GetObjectsInCell(cell);

        Assert.Contains(obj, objects);
        Assert.Single(objects);
    }

    [Fact]
    public void GetObjectsInCellEmptyPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var cell = new[] { 1, 2 };

        var objects = grid.GetObjectsInCell(cell);

        Assert.Empty(objects);
    }

    [Fact]
    public void GetCellPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);

        var cell = grid.GetCell([15, 25]);

        Assert.True(new[] { 1, 2 }.SequenceEqual(cell));
    }

    [Fact]
    public void GetNeighborCellsPositiveTest()
    {
        var grid = new SpatialPartitionGrid(10, 2);
        var center = new[] { 1, 1 };

        var neighbors = grid.GetNeighborCells(center);

        var expected = new[]
        {
            new[] { 0, 0 },
            [0, 1],
            [0, 2],
            [1, 0],
            [1, 2],
            [2, 0],
            [2, 1],
            [2, 2],
        };
        Assert.Equal(8, neighbors.Count);
        foreach (var expectedCell in expected)
        {
            Assert.Contains(neighbors, cell => cell.SequenceEqual(expectedCell));
        }
    }

    [Fact]
    public void CellsEqualPositiveTest()
    {
        var cell1 = new[] { 1, 2 };
        var cell2 = new[] { 1, 2 };

        var result = SpatialPartitionGrid.CellsEqual(cell1, cell2);

        Assert.True(result);
    }

    [Fact]
    public void CellsEqualDifCellsNegativeTest()
    {
        var cell1 = new[] { 1, 2 };
        var cell2 = new[] { 1, 3 };

        var result = SpatialPartitionGrid.CellsEqual(cell1, cell2);

        Assert.False(result);
    }

    [Fact]
    public void CellsEqualDifLenNegativeTest()
    {
        var cell1 = new[] { 1, 2 };
        var cell2 = new[] { 1, 2, 3 };

        var result = SpatialPartitionGrid.CellsEqual(cell1, cell2);

        Assert.False(result);
    }
}
