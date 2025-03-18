using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameObjectsRepositoryGet : ICommand
{
    public void Execute()
    {
        var gameObjectsRepository = new Dictionary<string, object>();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.Item.Get",
                (object[] args) =>
                {
                    var id = (string)args[0];

                    if (gameObjectsRepository.ContainsKey(id))
                    {
                        return gameObjectsRepository[id];
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
