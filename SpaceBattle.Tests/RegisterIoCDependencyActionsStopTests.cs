using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencySendActionStopTests
{
    public RegisterIoCDependencySendActionStopTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterIoCDependencySendActionStopPositiveTest()
    {
        var registerIoCDependencyActionsStop = new RegisterIoCDependencyActionsStop();
        registerIoCDependencyActionsStop.Execute();

        IDictionary<string, object> order = new Dictionary<string, object>();
        IDictionary<string, object> dict = new Dictionary<string, object>();
        var label = "endCommand";

        order["Dictionary"] = dict;
        order["Label"] = label;

        var resolveIoCDependencyActionStop = IoC.Resolve<ICommand>("Actions.Stop", order);
        Assert.NotNull(resolveIoCDependencyActionStop);
        Assert.IsType<EndCommand>(resolveIoCDependencyActionStop);
    }
}
