using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyCollisionCheck : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
        "IoC.Register",
        "Collision.Check",
        (object[] args) =>
        {
            var object1 = args[0];
            var object2 = args[1];

            var (values, treeType) = IoC.Resolve<(int[], string)>("Collision.Get.DeltaValuesAndTreeType", object1, object2);

            object tree = IoC.Resolve<IDictionary<int, object>>($"Collision.Get.Tree.{treeType}");

            return (object)values.All(value =>
            {
                if (tree is not IDictionary<int, object> dictionary)
                {
                    return false;
                }

                if (!dictionary.TryGetValue(value, out var subTree))
                {
                    return false;
                }

                tree = subTree;
                return true;
            });
        }
        ).Execute();
    }
}
