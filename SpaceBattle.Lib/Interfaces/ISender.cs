using Hwdtech;

namespace SpaceBattle.Lib;

public interface ISender
{
    void Add(ICommand cmd);
}
