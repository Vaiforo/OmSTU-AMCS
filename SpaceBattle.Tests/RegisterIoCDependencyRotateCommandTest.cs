using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyRotateCommandTests : IDisposable
{
    public RegisterIoCDependencyRotateCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RotateCommandRegisteredPositive()
    {
        var irotatingObject = new Mock<IRotatingObject>();
        var obj = new Mock<object>();
        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Adaters.IRotatingObject",
                (object[] args) => irotatingObject.Object
            )
            .Execute();

        new RegisterIoCDependencyRotateComand().Execute();

        var resolveDependency = Ioc.Resolve<Lib.ICommand>("Commands.Rotate", obj.Object);
        Assert.NotNull(resolveDependency);
        Assert.IsType<RotateCommand>(resolveDependency);
    }
        public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
