using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateComand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "Commands.Rotate",
            (object[] args) =>
                new RotateCommand(IoC.Resolve<IRotatingObject>("Adaters.IRotatingObject", args[0]))
            )
            .Execute();
    }
}
