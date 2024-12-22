using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMoveCommandTests
{
    public RegisterIoCDependencyMoveCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void MoveCommandRegisteredPositiveTest()
    {
        var imovingObject = new Mock<IMovingObject>();
        var obj = new Mock<object>();
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Adapters.IMovingObject",
                (object[] args) => imovingObject.Object
            )
            .Execute();

        new RegisterIoCDependencyMoveCommand().Execute();

        var resolveDependency = IoC.Resolve<MoveCommand>("Commands.Move", obj.Object);
        Assert.NotNull(resolveDependency);
        Assert.IsType<MoveCommand>(resolveDependency);
    }
}
