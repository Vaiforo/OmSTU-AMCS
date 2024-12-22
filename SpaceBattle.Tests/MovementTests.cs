// ТЕСТЫ ПИСАЛ МАКС
// просто мы в самом начале так немного странно из-за меня распреедлили кто что делал что у него тесты move у меня vector
// при том что vector он писал, а я move

using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class MovementTests
{
    [Fact]
    public void ObjectUpdatePositionPositiveTest()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Returns(new Vector(12, 5)).Verifiable();
        moving.SetupGet(x => x.Velocity).Returns(new Vector(-4, 1)).Verifiable();

        var cmd = new MoveCommand(moving.Object);
        cmd.Execute();

        moving.VerifySet(x => x.Position = new Vector(8, 6), Times.Once);
        moving.VerifyAll();
    }

    [Fact]
    public void ObjectPositionUnreadableNegativeTest()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Throws(() => new Exception()).Verifiable();
        moving.SetupGet(x => x.Velocity).Returns(new Vector(-4, 1)).Verifiable();

        var cmd = new MoveCommand(moving.Object);

        Assert.Throws<Exception>(() => cmd.Execute());
    }

    [Fact]
    public void ObjectVelocityUnreadableNegativeTest()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Returns(new Vector(12, 5)).Verifiable();
        moving.SetupGet(x => x.Velocity).Throws(() => new Exception()).Verifiable();

        var cmd = new MoveCommand(moving.Object);

        Assert.Throws<Exception>(() => cmd.Execute());
    }

    [Fact]
    public void ObjectUpdatePositionNegativeTest()
    {
        var moving = new Mock<IMovingObject>();

        moving.SetupGet(x => x.Position).Returns(new Vector(12, 5)).Verifiable();
        moving.SetupGet(x => x.Velocity).Returns(new Vector(-4, 1)).Verifiable();

        var cmd = new MoveCommand(moving.Object);

        moving
            .SetupSet(x => x.Position = new Vector(8, 6))
            .Throws(() => new Exception())
            .Verifiable();

        Assert.Throws<Exception>(cmd.Execute);
        moving.VerifyAll();
    }
}
