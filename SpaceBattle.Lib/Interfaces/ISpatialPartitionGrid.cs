namespace SpaceBattle.Lib;

public interface ISpatialPartitionGrid
{
    void AddToGrid(IMovingObject obj);
    void RemoveFromGrid(IMovingObject obj);
    void UpdatePosition(IMovingObject obj);
    List<IMovingObject> GetNearby(IMovingObject obj);
    IEnumerable<int[]> GetAllOccupiedCells();
    List<IMovingObject> GetObjectsInCell(int[] obj);
}
