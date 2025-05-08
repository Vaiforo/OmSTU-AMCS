namespace SpaceBattle.Lib;

public static class CollisionFileReader
{
    public static IEnumerable<(int, int, int, int)> ReadCollisions(string filePath)
    {
        var collisions = new List<(int, int, int, int)>();

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (
                parts.Length == 4
                && int.TryParse(parts[0], out var dx)
                && int.TryParse(parts[1], out var dy)
                && int.TryParse(parts[2], out var vx)
                && int.TryParse(parts[3], out var vy)
            )
            {
                collisions.Add((dx, dy, vx, vy));
            }
        }

        return collisions;
    }
}
