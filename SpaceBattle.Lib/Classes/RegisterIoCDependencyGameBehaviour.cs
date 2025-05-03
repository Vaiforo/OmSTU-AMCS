using Hwdtech;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameBehaviour : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register",
        "Game.GameBehaviour",
        (object[] args) => new GameBehaviourCommand().Execute
        ).Execute();
    }
}
