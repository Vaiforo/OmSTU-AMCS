using Hwdtech;

namespace SpaceBattle.Lib;

public class AuthCommand : ICommand
{
    private readonly string _userID;
    private readonly string _objectID;
    private readonly string _action;

    public AuthCommand(string userID, string objectID, string action)
    {
        _userID = userID;
        _objectID = objectID;
        _action = action;
    }

    public void Execute()
    {
        var accessedUsers = IoC.Resolve<IEnumerable<string>>(
            "GameItems.GetAccessUsers",
            $"{_objectID}.{_action}"
        );

        if (!accessedUsers.Contains(_userID))
        {
            throw new UnauthorizedAccessException(
                $"User {_userID} does not have access to action {_action} on object {_objectID}"
            );
        }
    }
}
