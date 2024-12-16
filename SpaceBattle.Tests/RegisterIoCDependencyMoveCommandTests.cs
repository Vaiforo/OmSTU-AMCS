using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMoveCommandTests
{
    public RegisterIoCDependencyMoveCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void MoveCommandRegisteredPositive()
    {
        var imovingObject = new Mock<IMovingObject>();
        var obj = new Mock<object>();
        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Adapters.IMovingObject",
                (object[] args) => imovingObject.Object
            )
            .Execute();

        new RegisterIoCDependencyMoveCommand().Execute();

        var resolveDependency = Ioc.Resolve<MoveCommand>("Commands.Move", obj.Object);
        Assert.NotNull(resolveDependency);
        Assert.IsType<MoveCommand>(resolveDependency);
    }
}
