using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyCollisionCheckTests
{
    public RegisterIoCDependencyCollisionCheckTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute(); 
    }

    [Fact]
    public void RegisterIoCDependencyCollisionCheckPositiveTest()
    {
        var RegisterIoCDependencyCollisionCheck = new RegisterIoCDependencyCollisionCheck();
        RegisterIoCDependencyCollisionCheck.Execute();

        int[] deltaValues = [];
        var tree = new Dictionary<int, object>();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Tree",
            (object[] args) =>
            {
                return tree;
            }
        ).Execute();

        var resolveIoCDependencyCollisionCheckPositive = IoC.Resolve<bool>(
            "Collision.Check", deltaValues
        );

        Assert.IsType<bool>(resolveIoCDependencyCollisionCheckPositive);
    }
}
