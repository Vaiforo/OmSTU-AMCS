using Hwdtech;

namespace SpaceBattle.Lib;

public interface IGameObjectRepository
{
    void AddGameObject(string str, Object object);
    object GetGameObject(string str);
    void RemoveGameObject(string str);
}