using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependenciesGameObjectsRepository : ICommand
{
    public void Execute()
    {
        var gameObjectsRepository = new Dictionary<string, object>();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Game.Item.Add",
                (object[] args) =>
                {
                    var id = (string)args[0];
                    var item = args[1];

                    if (gameObjectsRepository.ContainsKey(id))
                    {
                        throw new Exception("Object with id " + id + "already not exist");
                    }
                    else
                    {
                        gameObjectsRepository.Add(id, item);
                    }
                }
            )
            .Execute();

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
