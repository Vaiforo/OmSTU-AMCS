using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencySendCommandTests
{
    public RegisterIoCDependencySendCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterIoCDependencySendCommandResolvePositiveTest()
    {
        var commandMock = new Mock<ICommand>();
        var commandReceiverMock = new Mock<ICommandReciever>();

        var command = commandMock.Object;
        var commandReceiver = commandReceiverMock.Object;

        IoC.Resolve<ICommand>("IoC.Register", "Adapters.ICommand", (object[] args) => command)
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.ICommandReciever",
                (object[] args) => commandReceiver
            )
            .Execute();

        var registerIoCDependencySendCommand = new RegisterIoCDependencySendCommand();
        registerIoCDependencySendCommand.Execute();

        var resolveIoCDependencySendCommand = IoC.Resolve<ICommand>(
            "Commands.Send",
            new object[] { command, commandReceiver }
        );

        Assert.NotNull(resolveIoCDependencySendCommand);
        Assert.IsType<SendCommand>(resolveIoCDependencySendCommand);
        resolveIoCDependencySendCommand.Execute();
        commandReceiverMock.Verify(commandReciever => commandReciever.Recieve(command), Times.Once);
    }
}
