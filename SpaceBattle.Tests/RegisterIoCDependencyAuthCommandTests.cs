using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyAuthCommandTests
{
    public RegisterIoCDependencyAuthCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void AuthCommandRegisteredPositiveTest()
    {
        var userID = "123456789";
        var objectID = "123456789";
        var action = "Fire";

        IDictionary<string, object> order = new Dictionary<string, object>
        {
            ["UserID"] = userID,
            ["ObjectID"] = objectID,
            ["Action"] = action,
        };

        new RegisterIoCDependencyAuthCommand().Execute();

        var resolvedCommand = IoC.Resolve<ICommand>("Commands.AuthCommand", order);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<AuthCommand>(resolvedCommand);
    }
}
