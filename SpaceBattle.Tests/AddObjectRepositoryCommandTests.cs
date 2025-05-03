using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class AddObjectRepositoryCommandTests
{
    public AddObjectRepositoryCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void GameObjectRepositoryAddPositiveTest()
    {
        var repo = new Dictionary<string, object>();
        var itemId = "gameItemID";
        var gameItem = new object();
        var addCommand = new AddObjectRepositoryCommand(repo, itemId, gameItem);

        addCommand.Execute();

        Assert.True(repo.ContainsKey("gameItemID"));
    }

    [Fact]
    public void GameObjectRepositoryAddItemAlreadyExistTest()
    {
        var repo = new Dictionary<string, object>();
        var itemId = "gameItemID";
        var gameItem = new object();
        var addCommand = new AddObjectRepositoryCommand(repo, itemId, gameItem);

        repo.Add(itemId, gameItem);

        Assert.Throws<Exception>(() => addCommand.Execute());
    }
}
