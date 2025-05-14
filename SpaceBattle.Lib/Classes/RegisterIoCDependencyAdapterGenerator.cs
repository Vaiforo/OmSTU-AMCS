using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyAdapterGenerator : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "DI.Adpaters.GetString",
                (Func<object[], object>)(
                    static args =>
                    {
                        var interfaceType = (Type)args[0];
                        var generator = new AdapterGenerator();
                        var memberName = args.Length > 1 ? (string)args[1] : null;
                        var customImpl = args.Length > 2 ? (string)args[2] : null;
                        return generator.GenerateAdapterCode(
                            interfaceType,
                            (memberName, customImpl)
                        );
                    }
                )
            )
            .Execute();
    }
}
