using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class StartMovingTest
{
    [Fact]
    public void ObjectInitPositiveTest()
    {
        var dict = new Dictionary<string, object>{};
        var queue = new Mock<ISender>();
        var command = new Mock<ICommand>();

        var startMoving = new StartMoving(command.Object, dict, queue.Object);

        Assert.NotNull(startMoving);
    }

    [Fact]
    public void ObjectInitNegativeTest()
    {
        var dict = new Dictionary<string, object>{};
        var queue = new Mock<ISender>();
        var command = new Mock<ICommand>();

        var startMoving = new StartMoving(command.Object, dict, queue.Object);

        Assert.NotNull(startMoving);
    }
}
