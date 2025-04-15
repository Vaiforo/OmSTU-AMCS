using Hwdtech;

namespace SpaceBattle.Lib
{
    public class RegisterShootDependency : ICommand
    {
        public void Execute()
        {
            // Регистрируем зависимость для создания WeaponParameters
            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Weapon.Parameters.Create",
                    (object[] args) =>
                    {
                        return new WeaponParameters(
                            (Vector)args[0],
                            (Vector)args[1],
                            (double)args[2]
                        );
                    }
                )
                .Execute();

            // Регистрируем зависимость для создания ShootCommand
            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Command.Shoot",
                    (object[] args) =>
                    {
                        var parameters = IoC.Resolve<WeaponParameters>(
                            "Weapon.Parameters.Create",
                            args[0],
                            args[1],
                            args[2]
                        );
                        return new ShootCommand(parameters);
                    }
                )
                .Execute();
        }
    }
}
