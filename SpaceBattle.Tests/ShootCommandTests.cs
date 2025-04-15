using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Tests
{
    public class ShootCommandTests
    {
        public ShootCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();

            IoC.Resolve<ICommand>(
                    "Scopes.Current.Set",
                    IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
                )
                .Execute();
        }

        [Fact]
        public void Execute_ShouldCreateAndSetupWeaponCorrectly()
        {
            var spawnPosition = new Vector(new[] { 0, 0 });
            var direction = new Vector(new[] { 2, 1 });
            var projectileSpeed = 2.0;
            var weaponParameters = new WeaponParameters(spawnPosition, direction, projectileSpeed);

            var weaponMock = new Mock<IWeapon>();
            var setupCommandMock = new Mock<ICommand>();
            var addItemCommandMock = new Mock<ICommand>();

            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Weapon.Create",
                    (object[] args) =>
                    {
                        return weaponMock.Object;
                    }
                )
                .Execute();

            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Weapon.Setup",
                    (object[] args) =>
                    {
                        return setupCommandMock.Object;
                    }
                )
                .Execute();

            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Game.Item.Add",
                    (object[] args) =>
                    {
                        return addItemCommandMock.Object;
                    }
                )
                .Execute();

            var shootCommand = new ShootCommand(weaponParameters);
            shootCommand.Execute();

            weaponMock.Verify(
                w =>
                    w.Setup(
                        It.Is<WeaponParameters>(p =>
                            p.SpawnPosition == spawnPosition
                            && p.Direction == direction
                            && p.ProjectileSpeed == projectileSpeed
                        )
                    ),
                Times.Once()
            );

            setupCommandMock.Verify(c => c.Execute(), Times.Once());
            addItemCommandMock.Verify(c => c.Execute(), Times.Once());

            Assert.IsType<ShootCommand>(shootCommand);
        }
    }
}
