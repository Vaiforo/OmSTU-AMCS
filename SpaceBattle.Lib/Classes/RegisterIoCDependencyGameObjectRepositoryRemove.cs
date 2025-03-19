using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameObjectsRepositoryRemove : ICommand
{
    public readonly Dictionary<string, object> gameObjectsRepository = new Dictionary<string, object>();

    public RegisterIoCDependencyGameObjectsRepositoryRemove(Dictionary<string, object> _gameObjectsRepository)
    {
        gameObjectsRepository = _gameObjectsRepository;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.Item.Remove",
                (object[] args) =>
                {
                    var id = (string)args[0];

                    if (gameObjectsRepository.ContainsKey(id))
                    {
                        gameObjectsRepository.Remove(id);
                    }
                    else
                    {
                        throw new Exception("Object with id " + id + "does not exist");
                    }
                }
            )
            .Execute();
    }
}
