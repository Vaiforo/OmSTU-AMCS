using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGetDeltaValues : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
        "IoC.Register",
        "GetDeltaValues",
        (object[] args) =>
        {
            var obj1 = (IMovingObject)args[0];
            var obj2 = (IMovingObject)args[1];

            var dX = obj1.Position.GetCoords()[0] - obj2.Position.GetCoords()[0];
            var dY = obj1.Position.GetCoords()[1] - obj2.Position.GetCoords()[1];

            var dVx = obj1.Velocity.GetCoords()[0] - obj2.Velocity.GetCoords()[0];
            var dVy = obj1.Velocity.GetCoords()[1] - obj2.Velocity.GetCoords()[1];

            return new int[] {dX, dY, dVx, dVy};
        }
        ).Execute();
    }
}
