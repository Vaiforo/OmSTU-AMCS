namespace SpaceBattle.Lib;

class SendCommand(ICommand commandToSend, ICommandReciever commandReciever): ICommand
{
    public void Execute()
    {
        commandReciever.Recieve(commandToSend);
    }
}
