using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class MovementTests
{
    [Fact]
    public void ObjectUpdatePositionPositiveTest()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Returns(new Vector(12, 5)).Verifiable();
        moving.SetupGet(x => x.Velocity).Returns(new Vector(-7, 3)).Verifiable();

        var cmd = new MoveCommand(moving.Object);
        cmd.Execute();

        moving.VerifySet(x => x.Position = new Vector(5, 8), Times.Once);
        moving.VerifyAll();
    }

    [Fact]
    public void ObjectPositionUnreadableNegativeTest()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Throws(() => new InvalidOperationException()).Verifiable();
        moving.SetupGet(x => x.Velocity).Returns(new Vector(-7, 3)).Verifiable();

        var cmd = new MoveCommand(moving.Object);

        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void ObjectVelocityUnreadableNegativeTest()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Returns(new Vector(12, 5)).Verifiable();
        moving.SetupGet(x => x.Velocity).Throws(() => new InvalidOperationException()).Verifiable();

        var cmd = new MoveCommand(moving.Object);

        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void ObjectUpdatePositionNegativeTest()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Returns(new Vector(12, 5)).Verifiable();
        moving.SetupGet(x => x.Velocity).Returns(new Vector(-7, 3)).Verifiable();

        var cmd = new MoveCommand(moving.Object);

        moving.SetupSet(x => x.Position = new Vector(5, 8)).Throws(() => new Exception()).Verifiable();

        Assert.Throws<Exception>(cmd.Execute);
        moving.VerifyAll();
    }
}
