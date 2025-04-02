using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RemoveObjectRepositoryCommandTests
{
    public RemoveObjectRepositoryCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void GameObjectRepositoryRemovePositiveTest()
    {
        var repo = new Dictionary<string, object>();
        var itemId = "gameItemID";
        var removeCommand = new RemoveObjectRepositoryCommand(repo, itemId);

        repo.Add(itemId, new object());

        removeCommand.Execute();

        Assert.False(repo.ContainsKey("gameItemID"));
    }

    [Fact]
    public void GameObjectRepositoryRemoveItemDoesNotExistTest()
    {
        var repo = new Dictionary<string, object>();
        var itemId = "gameItemID";
        var removeCommand = new RemoveObjectRepositoryCommand(repo, itemId);

        Assert.Throws<Exception>(() => removeCommand.Execute());
    }
}
