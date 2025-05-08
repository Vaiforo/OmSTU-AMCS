using SpaceBattle.Lib;

public class CollisionFileReaderTests
{
    [Fact]
    public void ReadCollisions_ValidFile_ReturnsCorrectCollisions()
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllLines(tempFile, new[] { "1,2,3,4", "5,6,7,8", "-1,-2,-3,-4" });

        var collisions = CollisionFileReader.ReadCollisions(tempFile).ToList();

        Assert.Equal(3, collisions.Count);
        Assert.Contains((1, 2, 3, 4), collisions);
        Assert.Contains((5, 6, 7, 8), collisions);
        Assert.Contains((-1, -2, -3, -4), collisions);

        File.Delete(tempFile);
    }

    [Fact]
    public void ReadCollisions_EmptyFile_ReturnsEmptyList()
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, "");

        var collisions = CollisionFileReader.ReadCollisions(tempFile).ToList();

        // Assert
        Assert.Empty(collisions);

        File.Delete(tempFile);
    }

    [Fact]
    public void ReadCollisions_InvalidFormat_SkipsInvalidLines()
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllLines(tempFile, new[] { "1,2,3,4", "invalid,line", "5,6,7,8" });

        var collisions = CollisionFileReader.ReadCollisions(tempFile).ToList();

        Assert.Equal(2, collisions.Count);
        Assert.Contains((1, 2, 3, 4), collisions);
        Assert.Contains((5, 6, 7, 8), collisions);

        File.Delete(tempFile);
    }

    [Fact]
    public void ReadCollisions_NonExistentFile_ThrowsException()
    {
        var nonExistentFile = "non_existent_file.txt";

        Assert.Throws<FileNotFoundException>(
            () => CollisionFileReader.ReadCollisions(nonExistentFile).ToList()
        );
    }
}
