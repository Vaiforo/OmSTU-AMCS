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

            var weaponMock = new Mock<IWeapon>();
            weaponMock.Setup(w => w.SpawnPosition).Returns(spawnPosition);
            weaponMock.Setup(w => w.Direction).Returns(direction);
            weaponMock.Setup(w => w.ProjectileSpeed).Returns(projectileSpeed);

            var setupCommandMock = new Mock<ICommand>();

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
                    "Actions.Start",
                    (object[] args) =>
                    {
                        return setupCommandMock.Object;
                    }
                )
                .Execute();

            var shootCommand = new ShootCommand(weaponMock.Object);
            shootCommand.Execute();

            Assert.IsType<ShootCommand>(shootCommand);
            setupCommandMock.Verify(c => c.Execute(), Times.Exactly(2));
        }
    }
}
