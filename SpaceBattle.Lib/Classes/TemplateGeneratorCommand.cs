using Hwdtech;
using Scriban;

namespace SpaceBattle.Lib;

public class TemplateGeneratorCommand : ICommand
{
    private readonly string _templatePath;

    public TemplateGeneratorCommand(string templatePath)
    {
        _templatePath = templatePath ?? throw new ArgumentNullException(nameof(templatePath));
    }

    public void Execute()
    {
        var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _templatePath);

        if (!File.Exists(fullTemplatePath))
        {
            throw new FileNotFoundException($"Template file not found at: {fullTemplatePath}");
        }

        var templateContent = File.ReadAllText(fullTemplatePath);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.GenerateCode",
                new Func<object[], object>(args =>
                {
                    var target = (string)args[0];
                    var members = (List<object>)args[1];

                    var template = Template.Parse(templateContent);
                    var model = new { target, members };

                    return template.Render(model);
                })
            )
            .Execute();
    }
}
