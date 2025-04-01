using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyGameObjectsRepositoryRemoveTests
{
    public RegisterIoCDependencyGameObjectsRepositoryRemoveTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }
    
    
    [Fact]
    public void RegisterIoCDependencyGameObjectRepositoryRemovePositiveTest()
    {
        var repo = new Dictionary<string, object>();
        var registerDependencyCommandAdd = new RegisterIoCDependencyGameObjectsRepositoryAdd(repo);
        var registerDependencyCommandRemove = new RegisterIoCDependencyGameObjectsRepositoryRemove(repo);
        var itemId = "gameItemID";
        var gameItem = new object();

        registerDependencyCommandAdd.Execute();

        var addCommand = IoC.Resolve<ICommand>("Game.Item.Add", itemId, gameItem);
        addCommand.Execute();        

        registerDependencyCommandRemove.Execute();
        
        var removeCommand = IoC.Resolve<ICommand>("Game.Item.Remove", itemId);
        removeCommand.Execute();

        Assert.False(repo.ContainsKey("gameItemID"));
    }
}
