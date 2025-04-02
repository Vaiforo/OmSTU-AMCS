using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyGameObjectsRepositoryGetTests
{
    public RegisterIoCDependencyGameObjectsRepositoryGetTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }
    

    [Fact]
    public void RegisterIoCDependencyGameObjectRepositoryGetPositiveTest()
    {
        var repo = new Dictionary<string, object>();
        var registerDependencyCommandAdd = new RegisterIoCDependencyGameObjectsRepositoryAdd(repo);
        var registerDependencyCommandGet = new RegisterIoCDependencyGameObjectsRepositoryGet(repo);
        var itemId = "gameItemID";
        var gameItem = new object();

        registerDependencyCommandAdd.Execute();

        var addCommand = IoC.Resolve<ICommand>("Game.Item.Add", itemId, gameItem);
        addCommand.Execute();

        registerDependencyCommandGet.Execute();
        var recievedInfo = IoC.Resolve<object>("Game.Item.Get", itemId);

        Assert.True(recievedInfo == gameItem);
    }

        [Fact]
    public void RegisterIoCDependencyGameObjectRepositoryGetItemDoesNotExist()
    {
        var repo = new Dictionary<string, object>();
        var registerDependencyCommandGet = new RegisterIoCDependencyGameObjectsRepositoryGet(repo);
        var itemId = "gameItemID";
        var gameItem = new object();    

        registerDependencyCommandGet.Execute();

        Assert.Throws<Exception>(() => IoC.Resolve<object>("Game.Item.Get", itemId));
    }
}
