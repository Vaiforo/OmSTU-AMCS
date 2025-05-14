using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class AdapterGeneratorConfigTests
{
    [Fact]
    public void AddAndGetCustomMemberSuccess()
    {
        var config = new AdapterGeneratorConfig();
        var interfaceType = typeof(IMovingObject);
        var memberName = "Velocity";
        var customImpl = "public Vector Velocity { get => new Vector(1, 2); }";

        config.AddCustomMember(interfaceType, memberName, customImpl);
        var result = config.GetCustomMember(interfaceType, memberName);

        Assert.Equal(customImpl, result);
    }

    [Fact]
    public void GetCustomMemberNonExistingReturnsNull()
    {
        var config = new AdapterGeneratorConfig();
        var interfaceType = typeof(IMovingObject);
        var memberName = "NonExistentProperty";

        var result = config.GetCustomMember(interfaceType, memberName);

        Assert.Null(result);
    }
}
