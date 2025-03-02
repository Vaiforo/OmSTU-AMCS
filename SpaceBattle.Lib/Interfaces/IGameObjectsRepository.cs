using Hwdtech;

namespace SpaceBattle.Lib;

public interface IGameObjectRepository
{
    void AddGameObject();
    object GetGameObject();
    void RemoveGameObject();
}