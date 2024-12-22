using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class EmptyCommandTest
{
    [Fact]
    public void EmptyCommandInitPositiveTest()
    {
        var emptyCommand = new EmptyCommand();
        emptyCommand.Execute();
    }
}
