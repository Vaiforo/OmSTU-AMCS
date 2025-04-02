using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyGameCanContinueTests
{
    public RegisterIoCDependencyGameCanContinueTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterIoCDependencyGameCanContinuePositiveTestAndReturnsTrue()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Game.AllowedTime.Get", (object[] args) => () => 100).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Count", (object[] args) => () => 1).Execute();

        var registerIoCDependencyGameCanContinue = new RegisterIoCDependencyGameCanContinue();
        registerIoCDependencyGameCanContinue.Execute();

        var resolveIoCDependencyGameCanContinue = IoC.Resolve<Func<bool>>(
            "Game.CanContinue",
            (long)20
        )();

        Assert.True(resolveIoCDependencyGameCanContinue);
    }

    [Fact]
    public void RegisterIoCDependencyGameCanContinuePositiveTestAndReturnsFalse()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Game.AllowedTime.Get", (object[] args) => () => 100).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Count", (object[] args) => () => 1).Execute();

        var registerIoCDependencyGameCanContinue = new RegisterIoCDependencyGameCanContinue();
        registerIoCDependencyGameCanContinue.Execute();

        var resolveIoCDependencyGameCanContinue = IoC.Resolve<Func<bool>>(
            "Game.CanContinue",
            (long)120
        )();

        Assert.False(resolveIoCDependencyGameCanContinue);
    }
}
