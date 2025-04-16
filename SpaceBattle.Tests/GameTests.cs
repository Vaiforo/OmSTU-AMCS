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
    public void GameExecutesCommands()
    {
        var commandMock = new Mock<ICommand>();
        var gameBehaviour = new RegisterIoCDependencyGameBehaviour();
        gameBehaviour.Execute();
        var canContinue = new RegisterIoCDependencyGameCanContinue();
        canContinue.Execute();
        var count = 3;

        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Get", (object[] args) => commandMock.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.AllowedTime.Get", (object[] args) => (object)100).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Count", (object[] args) => (object)count).Execute();

        commandMock.Setup(c => c.Execute()).Callback(() => { count--; });

        var game = new Game(IoC.Resolve<object>("Scopes.Current"));
        game.Execute();

        commandMock.Verify(c => c.Execute(), Times.Exactly(3));
    }

    [Fact]
    public void ExceptionHandlerIsUsedWhenCommandThrowsException()
    {
        var commandMock = new Mock<ICommand>();
        var exceptionMock = new Mock<ICommand>();
        var gameBehaviour = new RegisterIoCDependencyGameBehaviour();
        gameBehaviour.Execute();
        var canContinue = new RegisterIoCDependencyGameCanContinue();
        canContinue.Execute();
        var count = 1;

        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Get", (object[] args) => commandMock.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.AllowedTime.Get", (object[] args) => (object)100).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Count", (object[] args) => (object)count).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "ExceptionHandler.Handle", (object[] args) => exceptionMock.Object).Execute();

        commandMock.Setup(c => c.Execute()).Callback(() => { count--; }).Throws(new Exception());

        var game = new Game(IoC.Resolve<object>("Scopes.Current"));
        game.Execute();

        exceptionMock.Verify(e => e.Execute(), Times.Once);
    }

    [Fact]
    public void CommandsDontExevuteWhenTimeIsOver()
    {
        var commandMock = new Mock<ICommand>();
        var gameBehaviour = new RegisterIoCDependencyGameBehaviour();
        gameBehaviour.Execute();
        var canContinue = new RegisterIoCDependencyGameCanContinue();
        canContinue.Execute();
        var count = 3;

        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Get", (object[] args) => commandMock.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.AllowedTime.Get", (object[] args) => (object)-1).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Count", (object[] args) => (object)count).Execute();

        commandMock.Setup(c => c.Execute()).Callback(() => { count--; });

        var game = new Game(IoC.Resolve<object>("Scopes.Current"));
        game.Execute();

        commandMock.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void GameExecutesSomeCommandsBeforeTimeQuantReached()
    {
        var commandMock = new Mock<ICommand>();
        var gameBehaviour = new RegisterIoCDependencyGameBehaviour();
        gameBehaviour.Execute();
        var canContinue = new RegisterIoCDependencyGameCanContinue();
        canContinue.Execute();
        var count = 10000;

        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Get", (object[] args) => commandMock.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.AllowedTime.Get", (object[] args) => (object)5).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Count", (object[] args) => (object)count).Execute();

        commandMock.Setup(c => c.Execute()).Callback(() => { count--; });

        var game = new Game(IoC.Resolve<object>("Scopes.Current"));
        game.Execute();

        commandMock.Verify(c => c.Execute(), Times.AtLeastOnce);
        Assert.True(count > 0 && count < 10000);
    }
}
