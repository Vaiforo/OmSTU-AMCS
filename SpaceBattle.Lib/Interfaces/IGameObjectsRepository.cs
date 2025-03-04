using Hwdtech;

namespace SpaceBattle.Lib;

public interface IGameObjectsRepository
{
    Dictionary<string, object> gameObjectRepository { get; }

    void AddGameObject(string str, object obj);
    object GetGameObject(string str);
    void RemoveGameObject(string str);
}
