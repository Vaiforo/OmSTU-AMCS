using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencySendCommandTests
{
    public RegisterIoCDependencySendCommandTests()
    {
        /*new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scopes.Current.Set", iocScope).Execute();*/
    }

    [Fact]
    public void RegisterIoCDependencySendCommandResolvePositiveTest()
    {
        var commandMock = new Mock<Lib.ICommand>();
        var messageReceiverMock = new Mock<ICommandReciever>();

        var command = commandMock.Object;
        var messageReceiver = messageReceiverMock.Object;

        var registerIoCDependencySendCommand = new RegisterIoCDependencySendCommand();
        registerIoCDependencySendCommand.Execute();

        var resolveIoCDependencySendCommand = Ioc.Resolve<App.ICommand>(
            "Commands.Send",
            new object[] { command, messageReceiver }
        );

        Assert.NotNull(resolveIoCDependencySendCommand);
        Assert.IsType<SendCommand>(resolveIoCDependencySendCommand);
        resolveIoCDependencySendCommand.Execute();
        messageReceiverMock.Verify(reciever => reciever.Recieve(command), Times.Once);
    }
}
