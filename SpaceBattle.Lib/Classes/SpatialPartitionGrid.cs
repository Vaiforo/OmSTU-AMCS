namespace SpaceBattle.Lib;

public class SpatialPartitionGrid : ISpatialPartitionGrid
{
    private readonly Dictionary<int, List<IMovingObject>> _cells = [];
    private readonly Dictionary<IMovingObject, int> _objectCells = [];
    private readonly double _cellSize;
    private readonly List<int[]> _neighborOffsets;

    public SpatialPartitionGrid(double cellSize, int dimensions)
    {
        _cellSize = cellSize;
        _neighborOffsets = GenerateNeighborOffsets(dimensions);
    }

    private static List<int[]> GenerateNeighborOffsets(int dim)
    {
        return Enumerable
            .Range(0, dim)
            .Aggregate(
                new[] { Array.Empty<int>() }.AsEnumerable(),
                (acc, _) =>
                    acc.SelectMany(a =>
                        Enumerable.Range(-1, 3).Select(d => a.Concat([d]).ToArray())
                    ),
                offsets => offsets.Where(offset => offset.Any(x => x != 0)).ToList()
            );
    }

    private static int GetCellKey(int[] cell)
    {
        return cell.Aggregate(
                new HashCode(),
                (hash, coord) =>
                {
                    hash.Add(coord);
                    return hash;
                }
            )
            .ToHashCode();
    }

    public void AddToGrid(IMovingObject obj)
    {
        var cell = GetCell(obj.Position.GetCoords());
        var key = GetCellKey(cell);
        var list = _cells.GetValueOrDefault(key) ?? [];
        list.Add(obj);
        _cells[key] = list;
        _objectCells[obj] = key;
    }

    public void RemoveFromGrid(IMovingObject obj)
    {
        if (_objectCells.TryGetValue(obj, out var key) && _cells.TryGetValue(key, out var list))
        {
            list.Remove(obj);
            if (list.Count == 0)
            {
                _cells.Remove(key);
            }

            _objectCells.Remove(obj);
        }
    }

    public void UpdatePosition(IMovingObject obj)
    {
        if (!_objectCells.TryGetValue(obj, out var oldKey))
        {
            return;
        }

        var newCell = GetCell(obj.Position.GetCoords());
        var newKey = GetCellKey(newCell);

        if (newKey != oldKey)
        {
            if (_cells.TryGetValue(oldKey, out var oldList))
            {
                oldList.Remove(obj);
                if (oldList.Count == 0)
                {
                    _cells.Remove(oldKey);
                }
            }

            var newList = _cells.GetValueOrDefault(newKey) ?? [];
            newList.Add(obj);
            _cells[newKey] = newList;
            _objectCells[obj] = newKey;
        }
    }

    public List<IMovingObject> GetNearby(IMovingObject obj)
    {
        if (!_objectCells.TryGetValue(obj, out var key))
        {
            return [];
        }

        var cell = GetCell(obj.Position.GetCoords());
        var neighbors = GetNeighborCells(cell);

        var nearbyObjects = neighbors
            .Select(GetObjectsInCell)
            .Concat([GetObjectsInCell(cell).Where(o => o != obj)])
            .SelectMany(list => list)
            .ToList();

        return nearbyObjects;
    }

    public IEnumerable<IMovingObject> GetAllObjects()
    {
        return _cells.Values.SelectMany(list => list);
    }

    public List<IMovingObject> GetObjectsInCell(int[] cell)
    {
        return _cells.GetValueOrDefault(GetCellKey(cell), []);
    }

    public int[] GetCell(int[] position)
    {
        return [.. position.Select((p, i) => (int)(p / _cellSize))];
    }

    public List<int[]> GetNeighborCells(int[] center)
    {
        return
        [
            .. _neighborOffsets.Select(offset => offset.Select((o, i) => center[i] + o).ToArray()),
        ];
    }

    public static bool CellsEqual(int[] a, int[] b)
    {
        return a.Length == b.Length && a.Zip(b, (x, y) => x == y).All(equal => equal);
    }
}
