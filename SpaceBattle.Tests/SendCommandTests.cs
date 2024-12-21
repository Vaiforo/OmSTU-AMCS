using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    public SendCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scopes.Current.Set", iocScope).Execute();
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

        commandRecieverMock.Verify(reciever => reciever.Recieve(cmd), Times.Once());
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
