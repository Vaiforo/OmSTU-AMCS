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
        var availablObjects = IoC.Resolve<IEnumerable<string>>(
            "GameSubjects.GetAvailableObjects",
            $"{_userID}.{_action}"
        );

        if (!availablObjects.Contains(_objectID))
        {
            throw new UnauthorizedAccessException(
                $"User {_userID} does not have access to action {_action} on object {_objectID}"
            );
        }
    }
}
