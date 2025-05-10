using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class TemplateGeneratorCommandTests
{
    public TemplateGeneratorCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();

        new TemplateGeneratorCommand("AdapterTemplate.scriban").Execute();
    }

    [Fact]
    public void TemplateGenForPropertyInterfaceTestPosoitive()
    {
        var members = new List<object>
        {
            new
            {
                IsProperty = true,
                IsMethod = false,
                Type = "int",
                Name = "Velocity",
                CanGet = true,
                CanSet = true,
            },
        };

        var code = IoC.Resolve<string>("Adapter.GenerateCode", "IMovingObject", members);

        Assert.Contains("class IMovingObjectAdapter : IMovingObject", code);
        Assert.Contains("int Velocity", code);
        Assert.Contains(
            "get => IoC.Resolve<int>(\"Object.GetProperty\", \"Velocity\", _adaptee);",
            code
        );
        Assert.Contains(
            "set => IoC.Resolve<ICommand>(\"Object.SetProperty\", \"Velocity\", _adaptee, value).Execute();",
            code
        );
    }

    [Fact]
    public void TemplateGenForMethodInterfaceTestPosoitive()
    {
        var members = new List<object>
        {
            new
            {
                IsProperty = false,
                IsMethod = true,
                ReturnType = "void",
                Name = "Move",
                Parameters = new List<object>(),
            },
        };

        var code = IoC.Resolve<string>("Adapter.GenerateCode", "IMovingObject", members);

        Assert.Contains("class IMovingObjectAdapter : IMovingObject", code);
        Assert.Contains("public void Move()", code);
        Assert.Contains("var command = IoC.Resolve<ICommand>(\"Adapter.Move\", _adaptee);", code);
        Assert.Contains("command.Execute();", code);
    }

    [Fact]
    public void TemplateGenForMethodAndPropInterfaceTestPosoitive()
    {
        var members = new List<object>
        {
            new
            {
                IsProperty = true,
                IsMethod = false,
                Type = "int",
                Name = "Velocity",
                CanGet = true,
                CanSet = true,
            },
            new
            {
                IsProperty = false,
                IsMethod = true,
                ReturnType = "void",
                Name = "Move",
                Parameters = new List<object>(),
            },
        };

        var code = IoC.Resolve<string>("Adapter.GenerateCode", "IMovingObject", members);

        Assert.Contains("class IMovingObjectAdapter : IMovingObject", code);
        Assert.Contains("int Velocity", code);
        Assert.Contains(
            "get => IoC.Resolve<int>(\"Object.GetProperty\", \"Velocity\", _adaptee);",
            code
        );
        Assert.Contains(
            "set => IoC.Resolve<ICommand>(\"Object.SetProperty\", \"Velocity\", _adaptee, value).Execute();",
            code
        );
        Assert.Contains("public void Move()", code);
        Assert.Contains("var command = IoC.Resolve<ICommand>(\"Adapter.Move\", _adaptee);", code);
        Assert.Contains("command.Execute();", code);
    }

    [Fact]
    public void TemplateGenTemplateNotFoundTestNegative()
    {
        var randomTemplate = "RandomTemplate.scriban";
        var setup = new TemplateGeneratorCommand(randomTemplate);

        var ex = Assert.Throws<FileNotFoundException>(() => setup.Execute());

        Assert.Contains("Template file not found at:", ex.Message);
        Assert.Contains(randomTemplate, ex.Message);
    }
}
