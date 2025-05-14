using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

public class RegisterIoCDependencyStorageTests
{
    public RegisterIoCDependencyStorageTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void Execute_RegistersAllCommands()
    {
        var registerCommand = new RegisterIoCDependencyStorage();
        registerCommand.Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Collisions.CreateFromFile", It.IsAny<object>());

        IoC.Resolve<ICommand>("IoC.Register", "Collisions.Create", It.IsAny<object>());

        IoC.Resolve<ICommand>("IoC.Register", "Collisions.Add", It.IsAny<object>());
    }

    [Fact]
    public void Execute_ShouldCreateCollisionsList()
    {
        var quadruples = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };

        var register = new RegisterIoCDependencyStorage();

        register.Execute();

        var testCollisions = IoC.Resolve<List<(int, int, int, int)>>(
            "Collisions.Create",
            quadruples
        );
        Assert.IsType<List<(int, int, int, int)>>(testCollisions);
        Assert.Equal(quadruples, testCollisions);
    }

    [Fact]
    public void Execute_ShouldCreateCollisionsFromFile()
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllLines(tempFile, new[] { "1,2,3,4", "5,6,7,8" });

        var register = new RegisterIoCDependencyStorage();

        register.Execute();

        var testCollisions = IoC.Resolve<List<(int, int, int, int)>>(
            "Collisions.CreateFromFile",
            tempFile
        );
        Assert.IsType<List<(int, int, int, int)>>(testCollisions);
        Assert.Contains((1, 2, 3, 4), testCollisions);
        Assert.Contains((5, 6, 7, 8), testCollisions);

        File.Delete(tempFile);
    }

    [Fact]
    public void Execute_ShouldAddCollisions()
    {
        var form1 = "ship";
        var form2 = "asteroid";

        var quadruples = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };

        var register = new RegisterIoCDependencyStorage();

        register.Execute();
        var testCommand = IoC.Resolve<ICommand>("Collisions.Add", form1, form2, quadruples);

        Assert.IsType<CollisionAddToStorageCommand>(testCommand);
    }
}
