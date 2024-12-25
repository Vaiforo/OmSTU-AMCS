using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class EndCommandTests
{
    public EndCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void EndCommandPositiveTest()
    {
        var dict = new Dictionary<string, object>();
        var commandMock = new Mock<ICommandInjectable>();

        var command = commandMock.Object;

        dict.Add("startCommand", command);

        var endCommand = new EndCommand(dict, "startCommand");
        endCommand.Execute();

        commandMock.Verify(
            command => command.Inject(IoC.Resolve<EmptyCommand>("Commands.Empty")),
            Times.Once
        );
    }
}
