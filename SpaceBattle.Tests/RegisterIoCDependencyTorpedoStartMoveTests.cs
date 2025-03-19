using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyStartMoveTests
{
    public RegisterIoCDependencyStartMoveTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void ActionsStartMoveRegisteredPositiveTest()
    {
        new RegisterIoCDependencyStartMove().Execute();

        var gameObject = new Mock<IMovingObject>();
        var dict = new Dictionary<string, object>();
        var queue = new Mock<ISender>();
        var label = "StartMoveTorpedo";

        IDictionary<string, object> order = new Dictionary<string, object>
        {
            ["GameObject"] = gameObject.Object,
            ["Dictionary"] = dict,
            ["Sender"] = queue.Object,
            ["Label"] = label,
        };

        var resolveDependency = IoC.Resolve<StartMove>("Actions.StartMove", order);
        Assert.NotNull(resolveDependency);
        Assert.IsType<StartMove>(resolveDependency);
    }
}
