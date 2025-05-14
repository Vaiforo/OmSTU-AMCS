using Hwdtech;

namespace SpaceBattle.Lib;

public static class CollisionTreeBuilder
{
    public static (string movingObject, string staticObject, List<(int, int, int, int)> collisions) BuildCollisionTree(string movingObject, string staticObject)
    {
        var storage = IoC.Resolve<ICollisionStorage>("Collisions.Storage");

        if (!storage.TryGetCollisions(movingObject, staticObject, out var collisions))
        {
            var directory = "game_collisions";
            var filename = $"{movingObject}_{staticObject}_collision.txt";
            var filePath = Path.Combine(directory, filename);

            if (File.Exists(filePath))
            {
                collisions = CollisionFileReader.ReadCollisions(filePath).ToList();
                storage.StoreCollision(movingObject, staticObject, collisions);
            }
            else
            {
                return (movingObject, staticObject, null)!;
            }
        }

        return (movingObject, staticObject, collisions);
    }
}
