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
                (object[] args) =>
                {
                    var id = (string)args[0];
                    var item = args[1];

                    if (gameObjectsRepository.ContainsKey(id))
                    {
                        throw new Exception("Object with id " + id + " already exist");
                    }
                    else
                    {
                        gameObjectsRepository.Add(id, item);
                    }
                }
            )
            .Execute();
    }
}
