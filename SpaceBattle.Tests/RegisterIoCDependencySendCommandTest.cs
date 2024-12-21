using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencySendCommandTests
{

    RegisterIoCDependencySendCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scopes.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterIoCDependencySendCommandResolvePositiveTest()
    {
        
    }
}
