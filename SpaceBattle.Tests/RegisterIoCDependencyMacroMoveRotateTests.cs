using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMacroMoveRotateTests
{
    public RegisterIoCDependencyMacroMoveRotateTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void MacroMoveRotateRegisteredPositiveTest()
    {
        var command1 = new Mock<ICommand>();
        var command2 = new Mock<ICommand>();
        var command3 = new Mock<ICommand>();
        var command4 = new Mock<ICommand>();

        IoC.Resolve<ICommand>("IoC.Register", "Command1", (object[] args) => command1.Object)
            .Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Command2", (object[] args) => command2.Object)
            .Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Command3", (object[] args) => command3.Object)
            .Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Command4", (object[] args) => command4.Object)
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Specs.Move",
                (object[] args) => new List<string> { "Command1", "Command2" }
            )
            .Execute();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Specs.Rotate",
                (object[] args) => new List<string> { "Command3", "Command4" }
            )
            .Execute();

        var registerMacroCommand = new RegisterIoCDependencyMacroCommand();
        registerMacroCommand.Execute();

        var RegisterIoCDependencyMacroMoveRotate = new RegisterIoCDependencyMacroMoveRotate();
        RegisterIoCDependencyMacroMoveRotate.Execute();

        IoC.Resolve<ICommand>("Macro.Move").Execute();
        command1.Verify(cmd => cmd.Execute(), Times.Once());
        command2.Verify(cmd => cmd.Execute(), Times.Once());

        IoC.Resolve<ICommand>("Macro.Rotate").Execute();
        command3.Verify(cmd => cmd.Execute(), Times.Once());
        command4.Verify(cmd => cmd.Execute(), Times.Once());
    }
}
