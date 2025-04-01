using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyGameObjectsRepositoryAddTests
{
    public RegisterIoCDependencyGameObjectsRepositoryAddTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterIoCDependencyGameObjectRepositoryAddPositiveTest()
    {
        var repo = new Dictionary<string, object>();
        var registerDependencyCommand = new RegisterIoCDependencyGameObjectsRepositoryAdd(repo);
        var itemId = "gameItemID";
        var gameItem = new object();

        registerDependencyCommand.Execute();

        var addCommand = IoC.Resolve<ICommand>("Game.Item.Add", itemId, gameItem);
        addCommand.Execute();

        Assert.True(repo.ContainsKey("gameItemID"));
    }
}
