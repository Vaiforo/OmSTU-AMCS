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
    public void MacroCommandRegisteredPositiveTest()
    {
        var enumerable = new Mock<IEnumerable<Lib.ICommand>>();
        var obj = new Mock<Lib.ICommand>();

        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Adapters.IEnumerable<ICommand>",
                (object[] args) => enumerable.Object
            ).Execute();

        var RegisterIoCDependencyMacroCommand = new RegisterIoCDependencyMacroCommand();
        RegisterIoCDependencyMacroCommand.Execute();

        var resolveIoCDependencyMacroCommand = IoC.Resolve<Lib.ICommand>(
            "Commands.Macro",
             new Lib.ICommand[] {obj.Object}
        );

        Assert.NotNull(resolveIoCDependencyMacroCommand);
        Assert.IsType<MacroCommand>(resolveIoCDependencyMacroCommand);
    }
} 