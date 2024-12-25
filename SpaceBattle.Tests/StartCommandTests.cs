using Hwdtech;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class StartCommandTest
{
    [Fact]
    public void StartCommandInitPositiveTest()
    {
        var cmd = new Mock<ICommand>();
        var dict = new Dictionary<string, object>();
        var queue = new Mock<ISender>();
        var label = "startCommand";

        var startCommand = new StartCommand(cmd.Object, dict, queue.Object, label);
        startCommand.Execute();

        Assert.True(dict.ContainsKey("startCommand"));
        Assert.Equal(cmd.Object, dict["startCommand"]);
        queue.Verify(q => q.Add(cmd.Object), Times.Once);
    }
}
