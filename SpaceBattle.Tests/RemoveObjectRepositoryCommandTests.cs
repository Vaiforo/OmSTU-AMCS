using Hwdtech;
using Hwdtech.Ioc;

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
    public void GameObjectRepositoryRemovePositiveTest() { }

    [Fact]
    public void GameObjectRepositoryRemoveItemDoesNotExistTest() { }
}
