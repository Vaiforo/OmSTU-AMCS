using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class MovementTests
{
    [Fact]
    public void ObjectUpdatePositionPositive()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Returns(new Vector(12, 5));
        moving.SetupGet(x => x.Velocity).Returns(new Vector(-7, 3));

        var cmd = new MoveCommand(moving.Object);
        cmd.Execute();

        moving.VerifySet(x => x.Position = new Vector(5, 8));
    }

    [Fact]
    public void ObjectPositionUnreadableNegative()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Returns(new InvalidOperationException());
        moving.SetupGet(x => x.Velocity).Returns(new Vector(-7, 3));

        var cmd = new MoveCommand(moving.Object);

        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }
}