using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyCommandInjectableCommandTests
{
    public RegisterIoCDependencyCommandInjectableCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterCommandInjectableCommandPositiveTest()
    {
        var temp = new RegisterIoCDependencyCommandInjectableCommand();
        temp.Execute();

        var injectableCommand1 = IoC.Resolve<Lib.ICommand>("Commands.CommandInjectable");
        var injectableCommand2 = IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var injectableCommand3 = IoC.Resolve<CommandInjectableCommand>(
            "Commands.CommandInjectable"
        );

        Assert.IsType<CommandInjectableCommand>(injectableCommand1);
        Assert.IsType<CommandInjectableCommand>(injectableCommand2);
        Assert.IsType<CommandInjectableCommand>(injectableCommand3);
    }
}
