namespace SpaceBattle.Lib;

public class AdapterGeneratorConfig
{
    private readonly Dictionary<(Type, string), string> _customMembers = [];

    public void AddCustomMember(Type interfaceType, string memberName, string customImplementation)
    {
        _customMembers[(interfaceType, memberName)] = customImplementation;
    }

    public string? GetCustomMember(Type interfaceType, string memberName)
    {
        return _customMembers.TryGetValue((interfaceType, memberName), out var implementation)
            ? implementation
            : null;
    }
}
