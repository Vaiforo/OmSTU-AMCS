using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Macro.Move",
                (object[] args) => new CreateMacroCommandStrategy("Move").Resolve(args)
            )
            .Execute();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Macro.Rotate",
                (object[] args) => new CreateMacroCommandStrategy("Rotate").Resolve(args)
            )
            .Execute();
    }
}
