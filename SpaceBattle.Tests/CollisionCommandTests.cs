using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CollisionCommandTests
{
    public CollisionCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void CollisionCommandPositiveTest()
    {
        var tree = new Dictionary<int, object>(){
            {1,new Dictionary<int, object>(){
                {5,new Dictionary<int, object>(){
                    {3, new Dictionary<int, object>(){
                        {5, new Dictionary<int, object>()}
                    }}
                }}
            }}
        };

        var commandMock = new Mock<ICommand>();
        var obj1 = new Mock<IMovingObject>();
        var obj2 = new Mock<IMovingObject>();

        obj1.SetupGet(o => o.Position).Returns(new Vector([5, 3]));
        obj1.SetupGet(o => o.Velocity).Returns(new Vector([7, 6]));
        obj2.SetupGet(o => o.Position).Returns(new Vector([4, -2]));
        obj2.SetupGet(o => o.Velocity).Returns(new Vector([4, 1]));

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Handle",
            (object[] args) =>
            {
                commandMock.Object.Execute();
                return new EmptyCommand();
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Tree",
            (object[] args) =>
            {
                return tree;
            }
        ).Execute();

        var getDeltaValues = new RegisterIoCDependencyGetDeltaValues();
        getDeltaValues.Execute();
        var collisionCheck = new RegisterIoCDependencyCollisionCheck();
        collisionCheck.Execute();

        var collisionCommand = new CollisionCommand(obj1.Object, obj2.Object);
        collisionCommand.Execute();

        commandMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void CollisionCommandNegativeTest()
    {
        var tree = new Dictionary<int, object>(){
            {1,new Dictionary<int, object>(){
                {5,new Dictionary<int, object>(){
                    {3, new Dictionary<int, object>(){
                        {5, new Dictionary<int, object>()}
                    }}
                }}
            }}
        };

        var commandMock = new Mock<ICommand>();
        var obj1 = new Mock<IMovingObject>();
        var obj2 = new Mock<IMovingObject>();

        obj1.SetupGet(o => o.Position).Returns(new Vector([500, 3]));
        obj1.SetupGet(o => o.Velocity).Returns(new Vector([7, 6]));
        obj2.SetupGet(o => o.Position).Returns(new Vector([4, -2]));
        obj2.SetupGet(o => o.Velocity).Returns(new Vector([4, 1]));

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Handle",
            (object[] args) =>
            {
                commandMock.Object.Execute();
                return new EmptyCommand();
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Tree",
            (object[] args) =>
            {
                return tree;
            }
        ).Execute();

        var getDeltaValues = new RegisterIoCDependencyGetDeltaValues();
        getDeltaValues.Execute();
        var collisionCheck = new RegisterIoCDependencyCollisionCheck();
        collisionCheck.Execute();

        var collisionCommand = new CollisionCommand(obj1.Object, obj2.Object);
        collisionCommand.Execute();

        commandMock.Verify(c => c.Execute(), Times.Never);
    }
}
