using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyAdapterGeneratorConfig : ICommand
{
    private readonly AdapterGeneratorConfig _config;

    public RegisterIoCDependencyAdapterGeneratorConfig(AdapterGeneratorConfig config)
    {
        _config = config;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register", "DI.Adpaters.Config", () => _config).Execute();
    }
}
