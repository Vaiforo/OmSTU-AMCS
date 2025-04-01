using Hwdtech;
using Hwdtech.Ioc;

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
        
    }

    [Fact]
    public void RegisterIoCDependencyGameObjectRepositoryRemoveItemDoesNotExistTest()
    {
        
    }
}
