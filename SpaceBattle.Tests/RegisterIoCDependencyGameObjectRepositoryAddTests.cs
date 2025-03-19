using Hwdtech;
using Hwdtech.Ioc;

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
}
