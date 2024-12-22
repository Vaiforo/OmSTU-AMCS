using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class EmptyCommandTest
{
    [Fact]
    public void EmptyCommandInitPositive()
    {
        var emptyCommand = new EmptyCommand();
        emptyCommand.Execute();
    }
}
