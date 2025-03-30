using Hwdtech;

namespace SpaceBattle.Lib;

public class AuthCommand : ICommand
{
    private readonly string _userID;
    private readonly string _objectID;
    private readonly string _actionID;

    public AuthCommand(string userID, string objectID, string actionID)
    {
        _userID = userID;
        _objectID = objectID;
        _actionID = actionID;
    }

    public void Execute()
    {
        var accessedUsers = IoC.Resolve<IEnumerable<string>>(
            "GameItems.GetAccessUsers",
            $"{_objectID}.{_actionID}"
        );

        if (!accessedUsers.Contains(_userID))
        {
            throw new UnauthorizedAccessException(
                $"User {_userID} does not have access to action {_actionID} on object {_objectID}"
            );
        }
    }
}
