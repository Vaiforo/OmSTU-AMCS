using System.Numerics;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class GameTests
{
    public GameTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void GameExecutesCommandPositive()
    {
        var command = new Mock<ICommand>();
        var queue = new Mock<IQueue>();

        queue.Setup(q => q.Count()).Returns(1);
        queue.Setup(q => q.Take()).Returns(command.Object).Callback(() => {queue.Setup(m => m.Count()).Returns(0);});

        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue", (object[] args) => queue.Object).Execute();

        var game = new Game(IoC.Resolve<object>("Scopes.Current"));

        game.Execute();

        command.Verify(cmd => cmd.Execute(), Times.Once);
    }

    [Fact]
    public void GameExecutesCommandNegative()
    {
        var command = new Mock<ICommand>();
        var queue = new Mock<IQueue>();
        var exceptionMessage = "err";

        command.Setup(cmd => cmd.Execute()).Throws(new Exception(exceptionMessage));
        queue.Setup(q => q.Count()).Returns(1);
        queue.Setup(q => q.Take()).Returns(command.Object).Callback(() => {queue.Setup(m => m.Count()).Returns(0);});

        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue", (object[] args) => queue.Object).Execute();

        var game = new Game(IoC.Resolve<object>("Scopes.Current"));

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        game.Execute();

        Assert.Contains(exceptionMessage, consoleOutput.ToString());
        command.Verify(cmd => cmd.Execute(), Times.Once);
    }
}