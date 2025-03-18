using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMacroCommandTests
{
    public RegisterIoCDependencyMacroCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void MacroCommandRegisteredPositiveTest()
    {
        var enumerable = new Mock<IEnumerable<ICommand>>();
        var obj = new Mock<ICommand>();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.IEnumerable<ICommand>",
                (object[] args) => enumerable.Object
            )
            .Execute();

        var RegisterIoCDependencyMacroCommand = new RegisterIoCDependencyMacroCommand();
        RegisterIoCDependencyMacroCommand.Execute();

        var resolveIoCDependencyMacroCommand = IoC.Resolve<ICommand>(
            "Commands.Macro",
            new ICommand[] { obj.Object }
        );

        Assert.NotNull(resolveIoCDependencyMacroCommand);
        Assert.IsType<MacroCommand>(resolveIoCDependencyMacroCommand);
    }
}
