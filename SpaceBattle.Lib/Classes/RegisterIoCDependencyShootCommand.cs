using Hwdtech;

namespace SpaceBattle.Lib
{
    public class RegisterShootDependency : ICommand
    {
        public void Execute()
        {
            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Command.Shoot",
                    (object[] args) =>
                    {
                        return new ShootCommand((Vector)args[0], (Vector)args[1], (double)args[2]);
                    }
                )
                .Execute();
        }
    }
}
