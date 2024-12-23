using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyRotateCommandTests
{
    public RegisterIoCDependencyRotateCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RotateCommandRegisteredPositive()
    {
        var irotatingObject = new Mock<IRotatingObject>();
        var obj = new Mock<object>();
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Adaters.IRotatingObject",
                (object[] args) => irotatingObject.Object
            )
            .Execute();

        var a = new RegisterIoCDependencyRotateComand();
        RegisterIoCDependencyRotateComand.Execute();

        var resolveDependency = IoC.Resolve<RotateCommand>("Commands.Rotate", obj.Object);
        Assert.NotNull(resolveDependency);
        Assert.IsType<RotateCommand>(resolveDependency);
    }
}
