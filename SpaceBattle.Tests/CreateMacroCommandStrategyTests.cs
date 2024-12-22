using SpaceBattle.Lib;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Tests;

public class CreateMacroCommandStrategyTests
{
    public CreateMacroCommandStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            ).Execute();
    }

    [Fact]
    public void MacroCommandResolvesAndAllCommandsCompletePositiveTest()
    {
        var command1 = new Mock<Lib.ICommand>();
        var command2 = new Mock<Lib.ICommand>();
        var command3 = new Mock<Lib.ICommand>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Specs.Test", (object[] args) => new List<string> {"Command1", "Command2", "Command3"}).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command1", (object[] args) => command1.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command2", (object[] args) => command2.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command3", (object[] args) => command3.Object).Execute();

        var registerMacroCommand = new RegisterIoCDependencyMacroCommand();
        registerMacroCommand.Execute();

        var CreateMacroCommandStrategy = new CreateMacroCommandStrategy("Test");
        CreateMacroCommandStrategy.Resolve([]).Execute();

        command1.Verify(cmd => cmd.Execute(), Times.Once());
        command2.Verify(cmd => cmd.Execute(), Times.Once());
        command3.Verify(cmd => cmd.Execute(), Times.Once());
    }

    [Fact]
    public void MacroCommandResolvesAndAllCommandsCompleteNegativeTest()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Specs.Test", (object[] args) => new List<string> {"Command"}).Execute();
        new RegisterIoCDependencyMacroCommand().Execute();

        var CreateMacroCommandStrategy = new CreateMacroCommandStrategy("Test");

        Assert.Throws<ArgumentException>(() => CreateMacroCommandStrategy.Resolve([]));
    }
}