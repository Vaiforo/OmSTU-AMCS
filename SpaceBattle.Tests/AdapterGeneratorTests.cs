using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class AdapterGeneratorTests
{
    public AdapterGeneratorTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();
        new GetAdaptersTemplateCommand().Execute(); // Register the template
    }

    [Fact]
    public void GenerateAdapterCode_NoCustomImpl_UsesStandard()
    {
        var config = new AdapterGeneratorConfig();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "DI.Adapters.Config",
                (Func<object[], object>)(args => config)
            )
            .Execute();

        var generator = new AdapterGenerator();
        var interfaceType = typeof(IMovingObject);

        var code = generator.GenerateAdapterCode(interfaceType);

        Assert.Contains("class IMovingObjectAdapter : IMovingObject", code);
        Assert.Contains("public SpaceBattle.Lib.Vector Position", code);
        Assert.Contains(
            "get => IoC.Resolve<SpaceBattle.Lib.Vector>(\"Object.GetProperty\", \"Position\", _adaptee);",
            code
        );
        Assert.Contains(
            "set => IoC.Resolve<ICommand>(\"Object.SetProperty\", \"Position\", _adaptee, value).Execute();",
            code
        );
        Assert.Contains("public SpaceBattle.Lib.Vector Velocity", code);
        Assert.Contains(
            "get => IoC.Resolve<SpaceBattle.Lib.Vector>(\"Object.GetProperty\", \"Velocity\", _adaptee);",
            code
        );
        Assert.DoesNotContain("set =>", code, StringComparison.Ordinal);
    }

    [Fact]
    public void GenerateAdapterCode_WithCustomImpl_UsesCustom()
    {
        var config = new AdapterGeneratorConfig();
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "DI.Adapters.Config",
                (Func<object[], object>)(args => config)
            )
            .Execute();

        var generator = new AdapterGenerator();
        var interfaceType = typeof(IMovingObject);
        var customImpl = "public Vector Velocity { get => new Vector(1, 2); }";

        var code = generator.GenerateAdapterCode(interfaceType, ("Velocity", customImpl));

        Assert.Contains("class IMovingObjectAdapter : IMovingObject", code);
        Assert.Contains("public Vector Velocity { get => new Vector(1, 2); }", code);
        Assert.Contains("public SpaceBattle.Lib.Vector Position", code);
        Assert.Contains(
            "get => IoC.Resolve<SpaceBattle.Lib.Vector>(\"Object.GetProperty\", \"Position\", _adaptee);",
            code
        );
        Assert.Contains(
            "set => IoC.Resolve<ICommand>(\"Object.SetProperty\", \"Position\", _adaptee, value).Execute();",
            code
        );
    }
}
