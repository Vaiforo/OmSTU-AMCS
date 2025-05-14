using Hwdtech;

namespace SpaceBattle.Lib;

public class AdapterGenerator
{
    private readonly string _template = IoC.Resolve<string>("DI.Adpaters.Template");

    public string GenerateAdapterCode(
        Type interfaceType,
        params (string? memberName, string? customImpl)[] customMembers
    )
    {
        var config = IoC.Resolve<AdapterGeneratorConfig>("DI.Adpaters.Config");
        var members = new List<string>();

        foreach (var (memberName, customImpl) in customMembers)
        {
            if (!string.IsNullOrEmpty(customImpl))
            {
                members.Add(customImpl);
            }
        }

        foreach (var prop in interfaceType.GetProperties())
        {
            var customImplFromConfig = config.GetCustomMember(interfaceType, prop.Name);
            if (customImplFromConfig != null && !members.Contains(customImplFromConfig))
            {
                members.Add(customImplFromConfig);
            }
            else if (!customMembers.Any(cm => cm.memberName == prop.Name))
            {
                var getAccessor = prop.CanRead
                    ? $"get => IoC.Resolve<{prop.PropertyType.FullName}>(\"Object.GetProperty\", \"{prop.Name}\", _adaptee);"
                    : "";
                var setAccessor = prop.CanWrite
                    ? $"set => IoC.Resolve<ICommand>(\"Object.SetProperty\", \"{prop.Name}\", _adaptee, value).Execute();"
                    : "";
                members.Add(
                    $@"
        public {prop.PropertyType.FullName} {prop.Name}
        {{
            {getAccessor}
            {setAccessor}
        }}
                "
                );
            }
        }

        var model = new { target = interfaceType.Name, members = members };
        var template = Scriban.Template.Parse(_template);
        return template.Render(model);
    }
}
