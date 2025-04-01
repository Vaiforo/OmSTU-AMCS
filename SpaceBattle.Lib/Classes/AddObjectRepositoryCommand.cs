using Hwdtech;

namespace SpaceBattle.Lib;

public class AddObjectRepositoryCommand : ICommand
{
    public readonly Dictionary<string, object> gameObjectsRepository = new Dictionary<string, object>();
    private readonly string id;
    private readonly object item;

    public AddObjectRepositoryCommand(Dictionary<string, object> _gameObjectsRepository, string _id, object _item)
    {
        gameObjectsRepository = _gameObjectsRepository;
        id = _id;
        item = _item;
    }

    public void Execute()
    {
        if (gameObjectsRepository.ContainsKey(id))
            {
                throw new Exception("Object with id " + id + " already exist");
            }
        else
            {
                gameObjectsRepository.Add(id, item);
            }
    }
}
