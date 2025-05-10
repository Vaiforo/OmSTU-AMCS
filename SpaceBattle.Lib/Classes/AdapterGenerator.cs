using Hwdtech;

namespace SpaceBattle.Lib;

public class AdapterGenerator
{
    public static string GetGeneratedCode(Type targetInterface)
    {
        if (!targetInterface.IsInterface)
        {
            throw new ArgumentException("Target must be an interface", nameof(targetInterface));
        }

        return IoC.Resolve<string>("Adapter.GenerateCode", targetInterface);
    }
}
