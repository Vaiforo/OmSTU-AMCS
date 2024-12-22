using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencySendCommandTests : IDisposable
{
    public RegisterIoCDependencySendCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scopes.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterIoCDependencySendCommandResolvePositiveTest()
    {
        var commandMock = new Mock<Lib.ICommand>();
        var commandReceiverMock = new Mock<ICommandReciever>();

        var command = commandMock.Object;
        var messageReceiver = commandReceiverMock.Object;

        var obj1 = new Mock<object>();
        var obj2 = new Mock<object>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.ICommand", (object[] args) => command)
            .Execute();

        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Adapters.ICommandReciever",
                (object[] args) => messageReceiver
            )
            .Execute();

        var registerIoCDependencySendCommand = new RegisterIoCDependencySendCommand();
        registerIoCDependencySendCommand.Execute();

        var resolveIoCDependencySendCommand = Ioc.Resolve<Lib.ICommand>(
            "Commands.Send",
            new object[] { obj1.Object, obj2.Object }
        );

        Assert.NotNull(resolveIoCDependencySendCommand);
        Assert.IsType<SendCommand>(resolveIoCDependencySendCommand);
        resolveIoCDependencySendCommand.Execute();
        commandReceiverMock.Verify(reciever => reciever.Recieve(command), Times.Once);
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
