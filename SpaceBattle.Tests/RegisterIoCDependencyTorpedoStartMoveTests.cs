using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyTorpedoStartMoveTests
{
    public RegisterIoCDependencyTorpedoStartMoveTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void ActionsMoveTorpedoRegisteredPositiveTest()
    {
        new RegisterIoCDependencyTorpedoStartMove().Execute();

        var torpedo = new Mock<IMovingObject>();
        var dict = new Dictionary<string, object>();
        var queue = new Mock<ISender>();

        IDictionary<string, object> order = new Dictionary<string, object>
        {
            ["Torpedo"] = torpedo.Object,
            ["Dictionary"] = dict,
            ["Sender"] = queue.Object,
        };

        var resolveDependency = IoC.Resolve<TorpedoStartMove>("Actions.MoveTorpedo", order);
        Assert.NotNull(resolveDependency);
        Assert.IsType<TorpedoStartMove>(resolveDependency);
    }
}
