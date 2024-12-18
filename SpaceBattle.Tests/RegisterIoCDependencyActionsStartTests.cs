using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyActionsStartTests : IDisposable
{
    public RegisterIoCDependencyActionsStartTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void ActionsStartRegisteredPositiveTest()
    {
        new RegisterIoCDependencyActionsStart().Execute();

        var cmd = new Mock<Lib.ICommand>();
        var dict = new Dictionary<string, object>();
        var queue = new Mock<ISender>();

        var order = new Dictionary<string, object>();
        order["Command"] = cmd.Object;
        order["Dictionary"] = dict;
        order["Sender"] = queue.Object;

        var resolveDependency = Ioc.Resolve<StartCommand>("Actions.Start", order);
        Assert.NotNull(resolveDependency);
        Assert.IsType<StartCommand>(resolveDependency);
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
