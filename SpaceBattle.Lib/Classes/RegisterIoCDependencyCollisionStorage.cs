using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyStorage : ICommand
{
    public void Execute()
    {
        var collisionStorage = new Dictionary<(string, string), List<(int, int, int, int)>>();
        var storage = new CollisionStorage(collisionStorage);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Collisions.CreateFromFile",
                (object[] args) =>
                {
                    var filePath = (string)args[0];
                    var collisions = CollisionFileReader.ReadCollisions(filePath);
                    return collisions;
                }
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Collisions.Create",
                (object[] args) =>
                {
                    var list = (IEnumerable<(int, int, int, int)>)args[0];
                    return new List<(int, int, int, int)>(list);
                }
            )
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Collisions.Add",
                (object[] args) =>
                {
                    var form1 = (string)args[0];
                    var form2 = (string)args[1];
                    var collisions = (List<(int, int, int, int)>)args[2];
                    return new CollisionAddToStorageCommand(form1, form2, collisions, storage);
                }
            )
            .Execute();
    }
}
