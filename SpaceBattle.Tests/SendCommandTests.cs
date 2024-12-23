using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    public SendCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void SendCommandSendedCommandToCommandRecieverPositiveTest()
    {
        var commandMock = new Mock<Lib.ICommand>();
        var cmd = commandMock.Object;

        var commandRecieverMock = new Mock<ICommandReciever>();
        var commandReciever = commandRecieverMock.Object;

        var sendCommand = new SendCommand(cmd, commandReciever);
        sendCommand.Execute();

        commandRecieverMock.Verify(commandReciever => commandReciever.Recieve(cmd), Times.Once());
    }

    [Fact]
    public void SendCommandSendedCommandToCommandRecieverICommandRecieverCannotGetCommandNegativeTest()
    {
        var commandMock = new Mock<Lib.ICommand>();
        var cmd = commandMock.Object;

        var commandRecieverMock = new Mock<ICommandReciever>();
        commandRecieverMock
            .Setup(commandReciever => commandReciever.Recieve(cmd))
            .Throws(() => new Exception())
            .Verifiable();
        var commandReciever = commandRecieverMock.Object;

        var sendCommand = new SendCommand(cmd, commandReciever);
        Assert.Throws<Exception>(() => sendCommand.Execute());
    }
}
