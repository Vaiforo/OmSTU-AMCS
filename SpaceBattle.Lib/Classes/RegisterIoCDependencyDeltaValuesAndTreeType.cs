using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyDeltaValuesAndTreeType : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
        "IoC.Register",
        "Collision.Get.TreeType",
        (object[] args) =>
        {
            var object1 = args[0];
            var object2 = args[1];

            var type1 = IoC.Resolve<string>("Object.Get.Type", object1);
            var type2 = IoC.Resolve<string>("Object.Get.Type", object2);

            var typeOrder = IoC.Resolve<IDictionary<(string, string), string>>("Collision.Get.typeOrder");

            var referenceType = typeOrder.TryGetValue((type1, type2), out var rt) ? 
            rt : typeOrder.TryGetValue((type2, type1), out rt) ? 
            rt : type1;

            var referenceObject = referenceType == type1 ? object1 : object2;
            var otherObject = referenceType == type1 ? object2 : object1;

            var positionReference = IoC.Resolve<int[]>("Object.Get.Position", referenceObject);
            var positionOther = IoC.Resolve<int[]>("Object.Get.Position", otherObject);

            var velocityReference = IoC.Resolve<int[]>("Object.Get.Velocity", referenceObject);
            var velocityOther = IoC.Resolve<int[]>("Object.Get.Velocity", otherObject);

            var deltaValues = positionReference
                .Zip(positionOther, (r, o) => r - o)
                .Concat(velocityReference
                .Zip(velocityOther, (r, o) => r - o))
                .ToArray();

            return (object)(deltaValues, $"{referenceType}{(referenceType == type1 ? type2 : type1)}");
        }
        ).Execute();
    }
}
