using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyActionsStartTests
{
    public RegisterIoCDependencyActionsStartTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void ActionsStartRegisteredPositiveTest()
    {
        new RegisterIoCDependencyActionsStart().Execute();

        var cmd = new Mock<Lib.ICommand>();
        var dict = new Dictionary<string, object>();
        var queue = new Mock<ISender>();
        var label = "startCommand";

        IDictionary<string, object> order = new Dictionary<string, object>
        {
            ["Command"] = cmd.Object,
            ["Dictionary"] = dict,
            ["Sender"] = queue.Object,
            ["Label"] = label,
        };

        var resolveDependency = IoC.Resolve<StartCommand>("Actions.Start", order);
        Assert.NotNull(resolveDependency);
        Assert.IsType<StartCommand>(resolveDependency);
    }
}
