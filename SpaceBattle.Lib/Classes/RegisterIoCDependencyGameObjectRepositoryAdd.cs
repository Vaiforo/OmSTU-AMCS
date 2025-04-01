using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameObjectsRepositoryAdd : ICommand
{
    public readonly Dictionary<string, object> gameObjectsRepository = new Dictionary<string, object>();

    public RegisterIoCDependencyGameObjectsRepositoryAdd(Dictionary<string, object> _gameObjectsRepository)
    {
        gameObjectsRepository = _gameObjectsRepository;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.Item.Add",
                (object[] args) => new AddObjectRepositoryCommand(gameObjectsRepository, (string)args[0], args[1])
            )
            .Execute();
    }
}
