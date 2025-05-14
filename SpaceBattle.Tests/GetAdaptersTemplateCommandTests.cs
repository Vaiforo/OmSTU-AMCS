using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class GetAdaptersTemplateCommandTests
{
    public GetAdaptersTemplateCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void Execute_RegistersTemplateContent_ReturnsExpectedString()
    {
        var expectedTemplateContent =
            @"
        using Hwdtech;

        class {{target}}Adapter : {{target}}
        {
            private IDictionary<string, object> _adaptee;

            public {{target}}Adapter(IDictionary<string, object> adaptee)
            {
                _adaptee = adaptee ?? throw new ArgumentNullException(nameof(adaptee));
            }

            {{ for member in members }}
                {{member}}
            {{ end }}
        }
        ";
        var command = new GetAdaptersTemplateCommand(expectedTemplateContent);

        command.Execute();

        var result = IoC.Resolve<string>("DI.Adpaters.Template");
        Assert.Equal(expectedTemplateContent, result);
    }

    [Fact]
    public void Execute_NullTemplateContent_ThrowsArgumentNullException()
    {
        string? templateContent = null;

        var exception = Assert.Throws<ArgumentNullException>(
            () => new GetAdaptersTemplateCommand(templateContent)
        );
        Assert.Equal("templateContent", exception.ParamName);
    }
}
