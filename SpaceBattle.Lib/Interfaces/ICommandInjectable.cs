namespace SpaceBattle.Lib;

public interface ICommandInjectable
{
    void Inject(ICommand command);
}
