using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class RotatingTests
{
    [Fact]
    public void ObjectUpdateRotationTest()
    {
        var rotating = new Mock<IRotatingObject>();

        rotating.SetupGet(x => x.Angle).Returns(new Angle(45));
        rotating.SetupGet(x => x.AngleVelocity).Returns(new Angle(90));

        var cmd = new RotateCommand(rotating.Object);
        cmd.Execute();

        rotating.VerifySet(x => x.Angle = new Angle(135));
    }

    [Fact]
    public void ObjectRotationAngleMissingTest()
    {
        var rotating = new Mock<IRotatingObject>();

        rotating.SetupGet(x => x.Angle).Throws(new InvalidOperationException());
        rotating.SetupGet(x => x.AngleVelocity).Returns(new Angle(90));

        var cmd = new RotateCommand(rotating.Object);

        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void ObjectRotationAngleVelocityMissingTest()
    {
        var rotating = new Mock<IRotatingObject>();

        rotating.SetupGet(x => x.Angle).Returns(new Angle(45));
        rotating.SetupGet(x => x.AngleVelocity).Throws(new InvalidOperationException());

        var cmd = new RotateCommand(rotating.Object);

        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }
}