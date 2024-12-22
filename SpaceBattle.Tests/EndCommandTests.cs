using System.Diagnostics;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class EndCommandTests
{
    [Fact]
    public void EndCommandPositiveTest()
    {
        var dict = new Dictionary<string, object>();
        var command = new CommandInjectableCommand();

        dict.Add("startCommand", command);

        var endCommand = new EndCommand(dict, "startCommand");
        endCommand.Execute();

        Assert.True(dict.ContainsKey("startCommand"));
        Assert.IsType<EmptyCommand>(command._command);
    }

    [Fact]
    public void EndCommandShouldStopCommandForConstantTimeTest()
    {
        var dict = new Dictionary<string, object>();
        var label = "endCommand";
        var command = new CommandInjectableCommand();
        dict.Add(label, command);

        var endCommand = new EndCommand(dict, label);
        var stopwatch = Stopwatch.StartNew();
        endCommand.Execute();
        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedTicks;
        Assert.True(elapsed < 10000, $"Execute took too long: {elapsed} ticks");
    }
}
