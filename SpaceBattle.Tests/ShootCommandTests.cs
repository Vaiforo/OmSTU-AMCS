﻿using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Tests
{
    public class shootCommandTests
    {
        public shootCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();

            IoC.Resolve<ICommand>(
                    "Scopes.Current.Set",
                    IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
                )
                .Execute();
        }

        [Fact]
        public void Execute_ShouldRegistershootCommandDependency()
        {
            new RegisterShootDependency().Execute();

            var position = new Vector(new[] { 0, 0 });
            var shootDirection = new Vector(new[] { 2, 1 });
            var speed = 2.0;
            var cmd = new Mock<ICommand>();
            var weaponMock = new Mock<IMovingObject>();
            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Weapon.Create",
                    (object[] args) =>
                    {
                        return new Dictionary<string, object> { { "Id", (string)args[0] } };
                    }
                )
                .Execute();
            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Adapters.IMovingObject",
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
                        return cmd.Object;
                    }
                )
                .Execute();

            IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "GameItem.Add",
                    (object[] args) =>
                    {
                        return cmd.Object;
                    }
                )
                .Execute();

            var shootCommand = new ShootCommand(position, shootDirection, speed);
            shootCommand.Execute();

            Assert.IsType<ShootCommand>(shootCommand);
        }

        [Fact]
        public void Execute_NotShouldRegistershootCommandDependency()
        {
            Assert.ThrowsAny<Exception>(() => IoC.Resolve<ICommand>("Command.Shoot"));
        }
    }
}
