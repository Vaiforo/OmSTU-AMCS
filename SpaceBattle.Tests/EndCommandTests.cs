using System.Diagnostics;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class EndCommandTests
{
    [Fact]
    public void EndCommandPositiveTest()
    {
        var dict = new Dictionary<string, object>();
        var commandMock = new Mock<ICommandInjectable>();

        var command = commandMock.Object;

        dict.Add("startCommand", command);

        var endCommand = new EndCommand(dict, "startCommand");
        endCommand.Execute();

    }
}
