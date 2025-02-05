using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RotatingTests
{
    [Fact]
    public void ObjectUpdateRotationPositiveTest()
    {
        var rotating = new Mock<IRotatingObject>();
        rotating.SetupGet(x => x.Angle).Returns(new Angle(45, 360));
        rotating.SetupGet(x => x.AngleVelocity).Returns(new Angle(90, 360));
        var cmd = new RotateCommand(rotating.Object);
        cmd.Execute();
        rotating.VerifySet(x => x.Angle = new Angle(135, 360), Times.Once);
        rotating.VerifyAll();
    }

    [Fact]
    public void ObjectRotationAngleMissingTest()
    {
        var rotating = new Mock<IRotatingObject>();
        rotating.SetupGet(x => x.Angle).Throws(new InvalidOperationException());
        rotating.SetupGet(x => x.AngleVelocity).Returns(new Angle(90, 360));
        var cmd = new RotateCommand(rotating.Object);
        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void ObjectRotationAngleVelocityMissingTest()
    {
        var rotating = new Mock<IRotatingObject>();
        rotating.SetupGet(x => x.Angle).Returns(new Angle(45, 360));
        rotating.SetupGet(x => x.AngleVelocity).Throws(new InvalidOperationException());
        var cmd = new RotateCommand(rotating.Object);
        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void ObjectUpdateRotationNegativeTest()
    {
        var rotating = new Mock<IRotatingObject>();
        rotating.SetupGet(x => x.Angle).Returns(new Angle(45, 360));
        rotating.SetupGet(x => x.AngleVelocity).Returns(new Angle(90, 360));
        var cmd = new RotateCommand(rotating.Object);
        cmd.Execute();
        rotating
            .SetupSet(x => x.Angle = new Angle(135, 360))
            .Throws(() => new Exception())
            .Verifiable();
        Assert.Throws<Exception>(cmd.Execute);
        rotating.VerifyAll();
    }
}
