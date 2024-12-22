using SpaceBattle.Lib;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMacroCommandTests
{
    public RegisterIoCDependencyMacroCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            ).Execute();
    }

    [Fact]
    public void MacroCommandRegisteredPositive()
    {
        var enumerableMock = new Mock<IEnumerable<Lib.ICommand>>();
        var enumerable = enumerableMock.Object;

        var obj = new Mock<object>();

        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Adapters.IEnumerable<ICommand>",
                (object[] args) => enumerable
            ).Execute();

        var RegisterIoCDependencyMacroCommand = new RegisterIoCDependencyMacroCommand();
        RegisterIoCDependencyMacroCommand.Execute();

        var resolveIoCDependencyMacroCommand = IoC.Resolve<Lib.ICommand>(
            "Commands.Macro",
            new object[] { obj.Object}
        );

        Assert.NotNull(resolveIoCDependencyMacroCommand);
        Assert.IsType<MacroCommand>(resolveIoCDependencyMacroCommand);
    }
} 