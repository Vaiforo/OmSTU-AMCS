using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class MacroCommandTests
{
    [Fact]
    public void MacroCommandPositiveTest()
    {
        var command1 = new Mock<ICommand>();
        var command2 = new Mock<ICommand>();
        var command3 = new Mock<ICommand>();

        var commands = new List<ICommand> { command1.Object, command2.Object, command3.Object };
        var macroCommand = new MacroCommand(commands);

        macroCommand.Execute();

        command1.Verify(cmd => cmd.Execute(), Times.Once);
        command2.Verify(cmd => cmd.Execute(), Times.Once);
        command3.Verify(cmd => cmd.Execute(), Times.Once);
    }

    [Fact]
    public void MacroCommandNegativeTest()
    {
        var command1 = new Mock<ICommand>();
        var command2 = new Mock<ICommand>();
        var command3 = new Mock<ICommand>();

        command1.Setup(cmd => cmd.Execute()).Verifiable();
        command2.Setup(cmd => cmd.Execute()).Throws(new Exception());
        command3.Setup(cmd => cmd.Execute()).Verifiable();

        var commands = new List<ICommand>{command1.Object, command2.Object, command3.Object};
        var macroCommand = new MacroCommand(commands);

        var ex = Assert.Throws<Exception>(() => macroCommand.Execute());

        command1.Verify(cmd => cmd.Execute(), Times.Once);
        command3.Verify(cmd => cmd.Execute(), Times.Never);
    }
}