using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameObjectsRepositoryAdd : ICommand
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
    }
}
