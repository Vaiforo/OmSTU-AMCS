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
            {-1,new Dictionary<int, object>(){
                {-5,new Dictionary<int, object>(){
                    {-3, new Dictionary<int, object>(){
                        {-5, new Dictionary<int, object>()}
                    }}
                }}
            }}
        };

        var typeOrder = new Dictionary<(string, string), string>
        {
            { ("Asteroid", "Ship"), "Ship" },
            { ("Ship", "Asteroid"), "Ship" }
        };

        var commandMock = new Mock<ICommand>();
        commandMock.Setup(c => c.Execute());

        var obj1 = new Mock<IDictionary<string, object>>();
        var obj2 = new Mock<IDictionary<string, object>>();

        obj1.Setup(obj => obj["Position"]).Returns(new int[] { 5, 3 });
        obj2.Setup(obj => obj["Position"]).Returns(new int[] { 4, -2 });

        obj1.Setup(obj => obj["Velocity"]).Returns(new int[] { 7, 6 });
        obj2.Setup(obj => obj["Velocity"]).Returns(new int[] { 4, 1 });

        obj1.Setup(obj => obj["Type"]).Returns("Asteroid");
        obj2.Setup(obj => obj["Type"]).Returns("Ship");

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Position", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Position"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Velocity", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Velocity"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Type", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (string)obj["Type"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Collision.Get.typeOrder", (object[] args) =>
            {
                return typeOrder;
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Handle",
            (object[] args) => commandMock.Object
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            $"Collision.Get.Tree.ShipAsteroid",
            (object[] args) =>
            {
                return tree;
            }
        ).Execute();

        var collisionCheck = new RegisterIoCDependencyCollisionCheck();
        collisionCheck.Execute();

        var deltaValuesAndTreeType = new RegisterIoCDependencyDeltaValuesAndTreeType();
        deltaValuesAndTreeType.Execute();

        var collisionCommand = new CollisionCommand(obj1.Object, obj2.Object);
        collisionCommand.Execute();

        commandMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void CollisionCommandNegativeTest()
    {
        var tree = new Dictionary<int, object>(){
            {-1,new Dictionary<int, object>(){
                {-5,new Dictionary<int, object>(){
                    {-3, new Dictionary<int, object>(){
                        {-500, new Dictionary<int, object>()}
                    }}
                }}
            }}
        };

        var typeOrder = new Dictionary<(string, string), string>
        {
            { ("Asteroid", "Ship"), "Ship" },
            { ("Ship", "Asteroid"), "Ship" }
        };

        var commandMock = new Mock<ICommand>();
        commandMock.Setup(c => c.Execute());

        var obj1 = new Mock<IDictionary<string, object>>();
        var obj2 = new Mock<IDictionary<string, object>>();

        obj1.Setup(obj => obj["Position"]).Returns(new int[] { 5, 3 });
        obj2.Setup(obj => obj["Position"]).Returns(new int[] { 4, -2 });

        obj1.Setup(obj => obj["Velocity"]).Returns(new int[] { 7, 6 });
        obj2.Setup(obj => obj["Velocity"]).Returns(new int[] { 4, 1 });

        obj1.Setup(obj => obj["Type"]).Returns("Asteroid");
        obj2.Setup(obj => obj["Type"]).Returns("Ship");

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Position", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Position"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Velocity", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Velocity"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Type", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (string)obj["Type"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Collision.Get.typeOrder", (object[] args) =>
            {
                return typeOrder;
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Handle",
            (object[] args) => commandMock.Object
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            $"Collision.Get.Tree.ShipAsteroid",
            (object[] args) =>
            {
                return tree;
            }
        ).Execute();

        var collisionCheck = new RegisterIoCDependencyCollisionCheck();
        collisionCheck.Execute();

        var deltaValuesAndTreeType = new RegisterIoCDependencyDeltaValuesAndTreeType();
        deltaValuesAndTreeType.Execute();

        var collisionCommand = new CollisionCommand(obj1.Object, obj2.Object);
        collisionCommand.Execute();

        commandMock.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void CollisionCommandNoTreeNegativeTest()
    {
        var tree = new Dictionary<int, object>(){
            {-1,new Dictionary<int, object>(){
                {-5,new Dictionary<int, object>(){
                    {-3, new Dictionary<int, int>(){
                        {-500, 200}
                    }}
                }}
            }}
        };

        var typeOrder = new Dictionary<(string, string), string>
        {
            { ("Asteroid", "Ship"), "Ship" },
            { ("Ship", "Asteroid"), "Ship" }
        };

        var commandMock = new Mock<ICommand>();
        commandMock.Setup(c => c.Execute());

        var obj1 = new Mock<IDictionary<string, object>>();
        var obj2 = new Mock<IDictionary<string, object>>();

        obj1.Setup(obj => obj["Position"]).Returns(new int[] { 5, 3 });
        obj2.Setup(obj => obj["Position"]).Returns(new int[] { 4, -2 });

        obj1.Setup(obj => obj["Velocity"]).Returns(new int[] { 7, 6 });
        obj2.Setup(obj => obj["Velocity"]).Returns(new int[] { 4, 1 });

        obj1.Setup(obj => obj["Type"]).Returns("Asteroid");
        obj2.Setup(obj => obj["Type"]).Returns("Ship");

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Position", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Position"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Velocity", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Velocity"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Type", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (string)obj["Type"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Collision.Get.typeOrder", (object[] args) =>
            {
                return typeOrder;
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Handle",
            (object[] args) => commandMock.Object
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            $"Collision.Get.Tree.ShipAsteroid",
            (object[] args) =>
            {
                return tree;
            }
        ).Execute();

        var collisionCheck = new RegisterIoCDependencyCollisionCheck();
        collisionCheck.Execute();

        var deltaValuesAndTreeType = new RegisterIoCDependencyDeltaValuesAndTreeType();
        deltaValuesAndTreeType.Execute();

        var collisionCommand = new CollisionCommand(obj1.Object, obj2.Object);
        collisionCommand.Execute();

        commandMock.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void CollisionCommandTypeOrderNegativeTest()
    {
        var tree = new Dictionary<int, object>(){
            {-1,new Dictionary<int, object>(){
                {-5,new Dictionary<int, object>(){
                    {-3, new Dictionary<int, object>(){
                        {-500, new Dictionary<int, object>()}
                    }}
                }}
            }}
        };

        var typeOrder = new Dictionary<(string, string), string>
        {

        };

        var commandMock = new Mock<ICommand>();
        commandMock.Setup(c => c.Execute());

        var obj1 = new Mock<IDictionary<string, object>>();
        var obj2 = new Mock<IDictionary<string, object>>();

        obj1.Setup(obj => obj["Position"]).Returns(new int[] { 5, 3 });
        obj2.Setup(obj => obj["Position"]).Returns(new int[] { 4, -2 });

        obj1.Setup(obj => obj["Velocity"]).Returns(new int[] { 7, 6 });
        obj2.Setup(obj => obj["Velocity"]).Returns(new int[] { 4, 1 });

        obj1.Setup(obj => obj["Type"]).Returns("Asteroid");
        obj2.Setup(obj => obj["Type"]).Returns("Ship");

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Position", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Position"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Velocity", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Velocity"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Type", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (string)obj["Type"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Collision.Get.typeOrder", (object[] args) =>
            {
                return typeOrder;
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Handle",
            (object[] args) => commandMock.Object
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            $"Collision.Get.Tree.AsteroidShip",
            (object[] args) =>
            {
                return tree;
            }
        ).Execute();

        var collisionCheck = new RegisterIoCDependencyCollisionCheck();
        collisionCheck.Execute();

        var deltaValuesAndTreeType = new RegisterIoCDependencyDeltaValuesAndTreeType();
        deltaValuesAndTreeType.Execute();

        var collisionCommand = new CollisionCommand(obj1.Object, obj2.Object);
        collisionCommand.Execute();

        commandMock.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void CollisionCommandTypeOrderPositiveTest()
    {
        var tree = new Dictionary<int, object>(){
                {-1,new Dictionary<int, object>(){
                    {-5,new Dictionary<int, object>(){
                        {-3, new Dictionary<int, object>(){
                            {-500, new Dictionary<int, object>()}
                        }}
                    }}
                }}
            };

        var typeOrder = new Dictionary<(string, string), string>
            {
                { ("Ship", "Asteroid"), "Ship" }
            };

        var commandMock = new Mock<ICommand>();
        commandMock.Setup(c => c.Execute());

        var obj1 = new Mock<IDictionary<string, object>>();
        var obj2 = new Mock<IDictionary<string, object>>();

        obj1.Setup(obj => obj["Position"]).Returns(new int[] { 5, 3 });
        obj2.Setup(obj => obj["Position"]).Returns(new int[] { 4, -2 });

        obj1.Setup(obj => obj["Velocity"]).Returns(new int[] { 7, 6 });
        obj2.Setup(obj => obj["Velocity"]).Returns(new int[] { 4, 1 });

        obj1.Setup(obj => obj["Type"]).Returns("Asteroid");
        obj2.Setup(obj => obj["Type"]).Returns("Ship");

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Position", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Position"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Velocity", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (int[])obj["Velocity"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Object.Get.Type", (object[] args) =>
            {
                var obj = (IDictionary<string, object>)args[0];
                return (string)obj["Type"];
            }
        ).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Collision.Get.typeOrder", (object[] args) =>
            {
                return typeOrder;
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Collision.Handle",
            (object[] args) => commandMock.Object
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            $"Collision.Get.Tree.ShipAsteroid",
            (object[] args) =>
            {
                return tree;
            }
        ).Execute();

        var collisionCheck = new RegisterIoCDependencyCollisionCheck();
        collisionCheck.Execute();

        var deltaValuesAndTreeType = new RegisterIoCDependencyDeltaValuesAndTreeType();
        deltaValuesAndTreeType.Execute();

        var collisionCommand = new CollisionCommand(obj1.Object, obj2.Object);
        collisionCommand.Execute();

        commandMock.Verify(c => c.Execute(), Times.Never);
    }
}
