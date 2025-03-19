using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class StartMoveTests
{
    public StartMoveTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void StartMoveInitPositiveTest()
    {
        new RegisterIoCDependencyActionsStart().Execute();

        var gameObject = new Mock<IMovingObject>();
        var dict = new Dictionary<string, object>();
        var sender = new Mock<ISender>();
        var label = "StartMoveTorpedo";

        var moveCommand = new Mock<ICommand>();
        var startMoveCommand = new Mock<ICommand>();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Move",
                (object[] args) => moveCommand.Object
            )
            .Execute();

        var command = new StartMove(gameObject.Object, dict, sender.Object, label);

        command.Execute();

        Assert.True(dict.ContainsKey("StartMoveTorpedo"));
        Assert.Equal(moveCommand.Object, dict["StartMoveTorpedo"]);
    }
}
