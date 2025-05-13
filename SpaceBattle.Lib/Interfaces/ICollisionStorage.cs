namespace SpaceBattle.Lib;

public interface ICollisionStorage
{
    void StoreCollision(string form1, string form2, List<(int, int, int, int)> collisions);
}
