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
            object tree = IoC.Resolve<IDictionary<int, object>>("Collision.Tree");

            var values = (int[])args[0];

            return (object)values.All(value =>
            {
                if(tree is not IDictionary<int, object> dictionary)
                {
                    return false;
                }

                if(!dictionary.TryGetValue(value, out var subTree))
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