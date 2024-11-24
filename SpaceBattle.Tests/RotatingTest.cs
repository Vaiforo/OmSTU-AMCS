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
        
        var cmd = new TurnCommand(rotating.Object);
        cmd.Execute();
  
        rotating.VerifySet(x => x.Angle = It.Is<Angle>(a => a.degrees == 135));
    }

    [Fact]
    public void ObjectRotationAngleMissingTest()
    {
        var rotating = new Mock<IRotatingObject>();
        rotating.SetupGet(x => x.Angle).Throws(new InvalidOperationException());
        rotating.SetupGet(x => x.AngleVelocity).Returns(new Angle(90));
        var cmd = new TurnCommand(rotating.Object);
        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }
    [Fact]
    public void ObjectRotationAngleVelocityMissingTest()
    {
        var rotating = new Mock<IRotatingObject>();
        rotating.SetupGet(x => x.Angle).Returns(new Angle(45));
        rotating.SetupGet(x => x.AngleVelocity).Throws(new InvalidOperationException());
        var cmd = new TurnCommand(rotating.Object);
        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }
    [Fact]
        public void AttemptToTurnUnmovableObjectThrowsException()
        {
            var rotating = new Mock<IRotatingObject>();
            rotating.SetupGet(x => x.Angle).Returns(new Angle(45));
            rotating.SetupGet(x => x.AngleVelocity).Returns(new Angle(90));
            rotating.SetupSet(x => x.Angle = It.IsAny<Angle>()).Throws<InvalidOperationException>();
            var cmd = new TurnCommand(rotating.Object);
            Assert.Throws<InvalidOperationException>(() => cmd.Execute());
        }
}