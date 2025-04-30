using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SpatialPartitionGridTests
{
    private readonly Mock<IMovingObject> _mockObject;
    private readonly SpatialPartitionGrid _spatialGrid;
    private readonly double _cellSize;
    private readonly int _dimensions;

    public SpatialPartitionGridTests()
    {
        _cellSize = 10.0;
        _dimensions = 2;
        _spatialGrid = new SpatialPartitionGrid(_cellSize, _dimensions);

        _mockObject = new Mock<IMovingObject>();
        _mockObject.Setup(m => m.Position).Returns(new Vector(5, 5));
        _mockObject.Setup(m => m.Velocity).Returns(new Vector(1, 0));
    }

    [Fact]
    public void AddToGridPositiveTest()
    {
        _spatialGrid.AddToGrid(_mockObject.Object);

        var cell = new int[] { 0, 0 };
        var objectsInCell = _spatialGrid.GetObjectsInCell(cell);
        Assert.Contains(_mockObject.Object, objectsInCell);
    }

    [Fact]
    public void RemoveFromGridPositiveTest()
    {
        _spatialGrid.AddToGrid(_mockObject.Object);
        _spatialGrid.RemoveFromGrid(_mockObject.Object);

        var cell = new int[] { 0, 0 };
        var objectsInCell = _spatialGrid.GetObjectsInCell(cell);
        Assert.DoesNotContain(_mockObject.Object, objectsInCell);
    }

    [Fact]
    public void UpdatePositionPositiveTest()
    {
        _mockObject.Setup(m => m.Position).Returns(new Vector(5, 5));
        _spatialGrid.AddToGrid(_mockObject.Object);

        _mockObject.Setup(m => m.Position).Returns(new Vector(15, 15));
        _spatialGrid.UpdatePosition(_mockObject.Object);

        var newCell = new int[] { 1, 1 };
        var objectsInNewCell = _spatialGrid.GetObjectsInCell(newCell);
        Assert.Contains(_mockObject.Object, objectsInNewCell);
    }

    [Fact]
    public void UpdatePositionSameCellTest()
    {
        var position = new Vector(5, 5);
        _mockObject.Setup(m => m.Position).Returns(position);

        _spatialGrid.AddToGrid(_mockObject.Object);
        _spatialGrid.UpdatePosition(_mockObject.Object);

        var cell = new int[] { 0, 0 };
        var objects = _spatialGrid.GetObjectsInCell(cell);
        Assert.Single(objects);
        Assert.Contains(_mockObject.Object, objects);
    }

    [Fact]
    public void UpdatePositionObjectNotInGridInitiallyTest()
    {
        _mockObject.Setup(m => m.Position).Returns(new Vector(30, 30));

        _spatialGrid.UpdatePosition(_mockObject.Object);

        var cell = new int[] { 3, 3 };
        var objects = _spatialGrid.GetObjectsInCell(cell);
        Assert.Single(objects);
        Assert.Contains(_mockObject.Object, objects);
    }

    [Fact]
    public void UpdatePositionRemovesFromOldCellTest()
    {
        _mockObject.Setup(m => m.Position).Returns(new Vector(5, 5));
        _spatialGrid.AddToGrid(_mockObject.Object);

        _mockObject.Setup(m => m.Position).Returns(new Vector(20, 0));
        _spatialGrid.UpdatePosition(_mockObject.Object);

        var oldCell = new int[] { 0, 0 };
        var newCell = new int[] { 2, 0 };

        Assert.Empty(_spatialGrid.GetObjectsInCell(oldCell));
        Assert.Contains(_mockObject.Object, _spatialGrid.GetObjectsInCell(newCell));
    }

    [Fact]
    public void GetNearbyPositiveTest()
    {
        _spatialGrid.AddToGrid(_mockObject.Object);

        var mockOtherObject = new Mock<IMovingObject>();
        mockOtherObject.Setup(m => m.Position).Returns(new Vector(15, 5));
        _spatialGrid.AddToGrid(mockOtherObject.Object);

        var nearbyObjects = _spatialGrid.GetNearby(_mockObject.Object);

        Assert.Contains(mockOtherObject.Object, nearbyObjects);
    }

    [Fact]
    public void GetAllOccupiedCellsPositiveTest()
    {
        _spatialGrid.AddToGrid(_mockObject.Object);

        var occupiedCells = _spatialGrid.GetAllOccupiedCells();

        Assert.Contains([0, 0], occupiedCells);
    }

    [Fact]
    public void CellsEqualNegativeTest()
    {
        int[] a = [1, 2, 3];
        int[] b = [1, 2];

        var result = SpatialPartitionGrid.CellsEqual(a, b);

        Assert.False(result);
    }

    [Fact]
    public void CellsEqualPositiveTest()
    {
        int[] a = [1, 2, 3];
        int[] b = [1, 2, 3];

        var result = SpatialPartitionGrid.CellsEqual(a, b);

        Assert.True(result);
    }
}
