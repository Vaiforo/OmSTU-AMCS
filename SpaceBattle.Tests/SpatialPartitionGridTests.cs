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
        _dimensions = 2; // Для 2D-решетки
        _spatialGrid = new SpatialPartitionGrid(_cellSize, _dimensions);

        _mockObject = new Mock<IMovingObject>();
        _mockObject.Setup(m => m.Position).Returns(new Vector(5, 5));
        _mockObject.Setup(m => m.Velocity).Returns(new Vector(1, 0));
    }

    [Fact]
    public void AddToGrid_ShouldAddObjectToCell()
    {
        // Act
        _spatialGrid.AddToGrid(_mockObject.Object);

        // Assert
        var cell = new int[] { 0, 0 }; // Для позиции (5,5) ячейка будет (0,0)
        var objectsInCell = _spatialGrid.GetObjectsInCell(cell);
        Assert.Contains(_mockObject.Object, objectsInCell);
    }

    [Fact]
    public void RemoveFromGrid_ShouldRemoveObjectFromCell()
    {
        // Act
        _spatialGrid.AddToGrid(_mockObject.Object);
        _spatialGrid.RemoveFromGrid(_mockObject.Object);

        // Assert
        var cell = new int[] { 0, 0 };
        var objectsInCell = _spatialGrid.GetObjectsInCell(cell);
        Assert.DoesNotContain(_mockObject.Object, objectsInCell);
    }

    [Fact]
    public void UpdatePosition_ShouldMoveObjectToNewCell()
    {
        // Arrange
        _spatialGrid.AddToGrid(_mockObject.Object);

        // Act
        _mockObject.Setup(m => m.Position).Returns(new Vector(15, 15)); // Новый объект в другой ячейке
        _spatialGrid.UpdatePosition(_mockObject.Object);

        // Assert
        var newCell = new int[] { 1, 1 };
        var objectsInNewCell = _spatialGrid.GetObjectsInCell(newCell);
        Assert.Contains(_mockObject.Object, objectsInNewCell);
    }

    [Fact]
    public void GetNearby_ShouldReturnObjectsInNeighboringCells()
    {
        // Arrange
        _spatialGrid.AddToGrid(_mockObject.Object);

        var mockOtherObject = new Mock<IMovingObject>();
        mockOtherObject.Setup(m => m.Position).Returns(new Vector(15, 5)); // Объект в соседней ячейке
        _spatialGrid.AddToGrid(mockOtherObject.Object);

        // Act
        var nearbyObjects = _spatialGrid.GetNearby(_mockObject.Object);

        // Assert
        Assert.Contains(mockOtherObject.Object, nearbyObjects);
    }

    [Fact]
    public void GetAllOccupiedCells_ShouldReturnAllCellsWithObjects()
    {
        // Arrange
        _spatialGrid.AddToGrid(_mockObject.Object);

        // Act
        var occupiedCells = _spatialGrid.GetAllOccupiedCells();

        // Assert
        Assert.Contains([0, 0], occupiedCells);
    }
}
