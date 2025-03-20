using Hwdtech;

namespace SpaceBattle.Lib;

public interface IQueue
{
    void Add(ICommand command);
    ICommand Take();
    int? Count();
}