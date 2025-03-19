using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class TorpedoStartMoveTests
{
    public TorpedoStartMoveTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void MoveTorpedoInitPositiveTest()
    {
        new RegisterIoCDependencyActionsStart().Execute();

        var torpedo = new Mock<IMovingObject>();
        var dict = new Dictionary<string, object>();
        var sender = new Mock<ISender>();

        var moveCommand = new Mock<ICommand>();
        var startMoveCommand = new Mock<ICommand>();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Move",
                (object[] args) => moveCommand.Object
            )
            .Execute();

        var command = new TorpedoStartMove(torpedo.Object, dict, sender.Object);

        command.Execute();

        Assert.True(dict.ContainsKey("MoveTorpedo"));
        Assert.Equal(moveCommand.Object, dict["MoveTorpedo"]);
    }
}
