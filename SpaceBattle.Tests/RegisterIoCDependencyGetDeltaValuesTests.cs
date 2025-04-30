using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyGetDeltaValuesTests
{
    public RegisterIoCDependencyGetDeltaValuesTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterIoCDependencyGetDeltaValuesPositiveTest()
    {
        var RegisterIoCDependencyGetDeltaValues = new RegisterIoCDependencyGetDeltaValues();
        RegisterIoCDependencyGetDeltaValues.Execute();

        var obj1 = new Mock<IMovingObject>();
        var obj2 = new Mock<IMovingObject>();

        obj1.SetupGet(o => o.Position).Returns(new Vector([5, 3]));
        obj1.SetupGet(o => o.Velocity).Returns(new Vector([7, 6]));
        obj2.SetupGet(o => o.Position).Returns(new Vector([4, -2]));
        obj2.SetupGet(o => o.Velocity).Returns(new Vector([4, 1]));

        var resolveRegisterIoCDependencyGetDeltaValues = IoC.Resolve<Array>(
            "GetDeltaValues", obj1.Object, obj2.Object
        );

        Assert.IsAssignableFrom<Array>(resolveRegisterIoCDependencyGetDeltaValues);
    }
}
