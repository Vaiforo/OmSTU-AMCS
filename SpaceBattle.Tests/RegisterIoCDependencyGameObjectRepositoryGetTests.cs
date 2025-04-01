using Hwdtech;
using Hwdtech.Ioc;

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
        
    }

    [Fact]
    public void RegisterIoCDependencyGameObjectRepositoryGetItemDoesNotExisTest()
    {
        
    }
}
