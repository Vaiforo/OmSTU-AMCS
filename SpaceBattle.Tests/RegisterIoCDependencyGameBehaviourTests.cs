using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyGameBehaviourTests
{
    public RegisterIoCDependencyGameBehaviourTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterIoCDependencyGameBehaviourPositiveTest()
    {
        var commandMock = new Mock<ICommand>();
        var exceptionMock = new Mock<ICommand>();

        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Get", (object[] args) => commandMock.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "ExceptionHandler.Handle", (object[] args) => exceptionMock.Object).Execute();

        var registerIoCDependencyGameBehaviour = new RegisterIoCDependencyGameBehaviour();
        registerIoCDependencyGameBehaviour.Execute();

        var resolveIoCDependencyGameBehaviour = IoC.Resolve<Action>(
            "Game.GameBehaviour"
        );

        Assert.NotNull(resolveIoCDependencyGameBehaviour);
        Assert.IsType<Action>(resolveIoCDependencyGameBehaviour);
    }
}
