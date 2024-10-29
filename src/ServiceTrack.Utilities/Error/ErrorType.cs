namespace ServiceTrack.Utilities.Error;

public enum ErrorType
{
    [ErrorMessage("User with this email already exists!")]
    EmailAlreadyExists,
    [ErrorMessage("Sorry this user doesn't exist!")]
    UserNotFound,
    [ErrorMessage("Sorry this password doesn't meet our requirements!")]
    PasswordDoesNotMeetRequirements,
    [ErrorMessage("Wrong password!")]
    InvalidPassword
}
