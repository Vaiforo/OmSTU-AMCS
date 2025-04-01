using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class AuthCommandTests
{
    public AuthCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void AuthPositiveTest()
    {
        var userID = "123456789";
        var objectID = "123456789";
        var action = "Fire";
        var accessedUsers = new List<string> { userID };

        var gameRepoReturner = new Mock<Func<string, IEnumerable<string>>>();
        gameRepoReturner.Setup(f => f(It.IsAny<string>())).Returns(accessedUsers);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "GameItems.GetAccessUsers",
                new Func<object[], object>(args => gameRepoReturner.Object((string)args[0]))
            )
            .Execute();

        var cmd = new AuthCommand(userID, objectID, action);
        cmd.Execute();
    }

    [Fact]
    public void AuthNegativeTest()
    {
        var userID = "123456789";
        var objectID = "123456789";
        var action = "Fire";
        var accessedUsers = new List<string> { "nullid" };

        var gameRepoReturner = new Mock<Func<string, IEnumerable<string>>>();
        gameRepoReturner.Setup(f => f(It.IsAny<string>())).Returns(accessedUsers);

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "GameItems.GetAccessUsers",
                new Func<object[], object>(args => gameRepoReturner.Object((string)args[0]))
            )
            .Execute();

        var cmd = new AuthCommand(userID, objectID, action);

        Assert.Throws<UnauthorizedAccessException>(() => cmd.Execute());
    }
}
