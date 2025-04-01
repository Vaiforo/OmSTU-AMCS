using Hwdtech;

namespace SpaceBattle.Lib;

public interface IQueue : ISender
{
    ICommand Take();
    int? Count();
}