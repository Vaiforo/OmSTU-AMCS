using Hwdtech;

namespace SpaceBattle.Lib;

public class GameObjectsRepository : IGameObjectsRepository
{
    public Dictionary<string, object> gameObjectRepository => throw new NotImplementedException();

    public void AddGameObject(string str, object obj)
    {
        gameObjectRepository.Add(str, obj);
    }

    public object GetGameObject(string str)
    {
        object val;
        gameObjectRepository.TryGetValue(str, out val);

        return val;
    }

    public void RemoveGameObject(string str)
    {
        gameObjectRepository.Remove(str);
    }
}
