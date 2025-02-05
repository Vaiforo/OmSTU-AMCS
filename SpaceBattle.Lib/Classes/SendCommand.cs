using Hwdtech;

namespace SpaceBattle.Lib;

public class SendCommand(ICommand commandToSend, ICommandReciever commandReciever) : ICommand
{
    public void Execute()
    {
        commandReciever.Recieve(commandToSend);
    }
}
