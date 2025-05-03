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
                (object[] args) => new RemoveObjectRepositoryCommand(gameObjectsRepository, (string)args[0])
            )
            .Execute();
    }
}
