using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyShootCommandTests
{
    public RegisterIoCDependencyShootCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterIoCDependencyShootCommandResolvePositiveTest()
    {
        new RegisterShootDependency().Execute();
        var position = new Vector(new[] { 0, 0 });
        var shootDirection = new Vector(new[] { 1, 1 });
        var speed = 2.0;
        var shootCommand = IoC.Resolve<ICommand>("Command.Shoot", position, shootDirection, speed);

        Assert.IsType<ShootCommand>(shootCommand);
    }

    [Fact]
    public void RegisterIoCDependencyShootCommandResolveNegativeTest()
    {
        Assert.ThrowsAny<Exception>(() => IoC.Resolve<ICommand>("Command.Shoot"));
    }
}
