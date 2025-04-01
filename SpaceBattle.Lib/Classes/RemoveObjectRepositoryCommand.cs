using Hwdtech;

namespace SpaceBattle.Lib;

public class RemoveObjectRepositoryCommand : ICommand
{
    public readonly Dictionary<string, object> gameObjectsRepository =
        new Dictionary<string, object>();
    private readonly string id;

    public RemoveObjectRepositoryCommand(
        Dictionary<string, object> _gameObjectsRepository,
        string _id
    )
    {
        gameObjectsRepository = _gameObjectsRepository;
        id = _id;
    }

    public void Execute()
    {
        if (gameObjectsRepository.ContainsKey(id))
        {
            gameObjectsRepository.Remove(id);
        }
        else
        {
            throw new Exception("Object with id " + id + " does not exist");
        }
    }
}
