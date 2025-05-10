using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class AdapterGeneratorTests
{
    public AdapterGeneratorTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void GenerateAdapterTestPositive()
    {
        var expectedCode =
            @"
            using Hwdtech;

            class IMovingObjectAdapter : IMovingObject
            {
                private IDictionary<string, object> _adaptee;

                public IMovingObjectAdapter(IDictionary<string, object> adaptee)
                {
                    _adaptee = adaptee ?? throw new ArgumentNullException(nameof(adaptee));
                }

                SpaceBattle.Lib.Vector Position
                {
                    get => IoC.Resolve<SpaceBattle.Lib.Vector>(""Object.GetProperty"", ""Position"", _adaptee);
                    set => IoC.Resolve<ICommand>(""Object.SetProperty"", ""Position"", _adaptee, value).Execute();
                }

                SpaceBattle.Lib.Vector Velocity
                {
                    get => IoC.Resolve<SpaceBattle.Lib.Vector>(""Object.GetProperty"", ""Velocity"", _adaptee);
                }
            }
            ";

        var mockFunc = new Mock<Func<object[], object>>();
        mockFunc
            .Setup(f => f(It.Is<object[]>(args => args[0] == typeof(IMovingObject))))
            .Returns(expectedCode);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.GenerateCode",
                new Func<object[], object>(args => mockFunc.Object(args))
            )
            .Execute();

        var code = AdapterGenerator.GetGeneratedCode(typeof(IMovingObject));

        Assert.Equal(expectedCode, code);

        mockFunc.Verify(
            f => f(It.Is<object[]>(args => args[0] == typeof(IMovingObject))),
            Times.Once()
        );
    }

    [Fact]
    public void GenerateAdapterFakeInterfaceTestNegative()
    {
        Assert.Throws<ArgumentException>(() => AdapterGenerator.GetGeneratedCode(typeof(string)));
    }

    [Fact]
    public void GenerateAdapterFakeKeyTestNegative()
    {
        Assert.Throws<ArgumentException>(
            () => AdapterGenerator.GetGeneratedCode(typeof(IMovingObject))
        );
    }
}
