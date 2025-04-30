namespace SpaceBattle.Lib;

public class SpatialPartitionGrid : ISpatialPartitionGrid
{
    private readonly Dictionary<string, List<IMovingObject>> _cells = [];
    private readonly double _cellSize;
    private readonly int _dimensions;

    public SpatialPartitionGrid(double cellSize, int dimensions)
    {
        _cellSize = cellSize;
        _dimensions = dimensions;
    }

    public void AddToGrid(IMovingObject obj)
    {
        var cell = GetCell(obj.Position.GetCoords());
        AddToCell(cell, obj);
    }

    public void RemoveFromGrid(IMovingObject obj)
    {
        var cell = GetCell(obj.Position.GetCoords());
        RemoveFromCell(cell, obj);
    }

    public void UpdatePosition(IMovingObject obj)
    {
        int[]? oldCell = null;

        foreach (var cell in _cells)
        {
            if (cell.Value.Contains(obj))
            {
                oldCell = ParseKey(cell.Key);
                break;
            }
        }

        var newCell = GetCell(obj.Position.GetCoords());

        if (oldCell == null || !CellsEqual(oldCell, newCell))
        {
            if (oldCell != null)
            {
                RemoveFromCell(oldCell, obj);
            }

            AddToCell(newCell, obj);
        }
    }

    public List<IMovingObject> GetNearby(IMovingObject obj)
    {
        var center = GetCell(obj.Position.GetCoords());
        var neighbors = GetNeighborCells(center);
        var result = new List<IMovingObject>();

        foreach (var cell in neighbors)
        {
            if (_cells.TryGetValue(Key(cell), out var list))
            {
                result.AddRange(list);
            }
        }

        return result;
    }

    public IEnumerable<int[]> GetAllOccupiedCells()
    {
        return _cells.Keys.Select(ParseKey);
    }

    public List<IMovingObject> GetObjectsInCell(int[] cell)
    {
        return _cells.TryGetValue(Key(cell), out var list) ? list : [];
    }

    public int[] GetCell(int[] position)
    {
        var cell = new int[_dimensions];
        for (var i = 0; i < _dimensions; i++)
        {
            cell[i] = (int)(position[i] / _cellSize);
        }

        return cell;
    }

    public List<int[]> GetNeighborCells(int[] center)
    {
        var result = new List<int[]>();
        GenerateOffsets(_dimensions, new int[_dimensions], 0, center, result);
        return result;
    }

    public static void GenerateOffsets(
        int dim,
        int[] offset,
        int idx,
        int[] center,
        List<int[]> result
    )
    {
        if (idx == dim)
        {
            var cell = new int[dim];
            for (var i = 0; i < dim; i++)
            {
                cell[i] = center[i] + offset[i];
            }

            result.Add(cell);
            return;
        }

        for (var d = -1; d <= 1; d++)
        {
            offset[idx] = d;
            GenerateOffsets(dim, offset, idx + 1, center, result);
        }
    }

    public void AddToCell(int[] cell, IMovingObject obj)
    {
        var key = Key(cell);
        if (!_cells.TryGetValue(key, out var list))
        {
            list = [];
            _cells[key] = list;
        }

        list.Add(obj);
    }

    public void RemoveFromCell(int[] cell, IMovingObject obj)
    {
        var key = Key(cell);
        if (_cells.TryGetValue(key, out var list))
        {
            list.Remove(obj);
            if (list.Count == 0)
            {
                _cells.Remove(key);
            }
        }
    }

    public static bool CellsEqual(int[] a, int[] b)
    {
        if (a.Length != b.Length)
        {
            return false;
        }

        for (var i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }

        return true;
    }

    public static string Key(int[] cell) => string.Join(",", cell);

    public static int[] ParseKey(string key) => [.. key.Split(',').Select(int.Parse)];
}
