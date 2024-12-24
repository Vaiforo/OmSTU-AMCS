using Hwdtech;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    [Fact]
    public void SendCommandSendedCommandToCommandRecieverPositiveTest()
    {
        var commandMock = new Mock<ICommand>();
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
        var commandMock = new Mock<ICommand>();
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
