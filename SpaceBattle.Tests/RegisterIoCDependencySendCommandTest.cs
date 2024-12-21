using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencySendCommandTests
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
        var cmd = commandMock.Object;

        var commandRecieverMock = new Mock<ICommandReciever>();
        var commandReciever = commandRecieverMock.Object;

        new SendCommand(cmd, commandReciever).Execute();

        commandRecieverMock.Verify(reciever => reciever.Recieve(cmd), Times.Once());
    }
}
