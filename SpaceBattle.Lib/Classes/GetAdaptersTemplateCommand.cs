using Hwdtech;

namespace SpaceBattle.Lib;

public class GetAdaptersTemplateCommand : ICommand
{
    private readonly string _templateContent;

    public GetAdaptersTemplateCommand(string? templateContent)
    {
        _templateContent =
            templateContent ?? throw new ArgumentNullException(nameof(templateContent));
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register",
                "DI.Adpaters.Template",
                (Func<object[], object>)(args => _templateContent)
            )
            .Execute();
    }
}
