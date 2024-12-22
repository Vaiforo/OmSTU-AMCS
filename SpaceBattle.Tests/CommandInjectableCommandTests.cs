using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CommandInjectableCommandTests
{
    [Fact]
    public void InjectPositiveTest()
    {
        var command = new Mock<ICommand>();
        var injectableCommand = new CommandInjectableCommand();
        injectableCommand.Inject(command.Object);
        injectableCommand.Execute();

        command.Verify(x => x.Execute(), Times.Once);
    }

    [Fact]
    public void InjectWithoutCommandNegativeTest()
    {
        var injectableCommand = new CommandInjectableCommand();

        Assert.Throws<NullReferenceException>(injectableCommand.Execute);
    }
}
