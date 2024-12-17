using App;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateComand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Rotate",
            (object[] args) =>
                new RotateCommand(Ioc.Resolve<IRotatingObject>("Adaters.IRotatingObject", args[0]))
            )
            .Execute();
    }
}