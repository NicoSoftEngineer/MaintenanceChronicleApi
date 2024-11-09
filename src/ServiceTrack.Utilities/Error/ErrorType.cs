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
    InvalidPassword,
    [ErrorMessage("Sorry this token is invalid!")]
    InvalidEmailConfirmationToken,
    [ErrorMessage("Name must be unique!")]
    NameMustBeUnique,
    [ErrorMessage("Sorry this tenant doesn't exist!")]
    TenantNotFound,
    [ErrorMessage("Sorry your identity couldn't be accessed!")]
    InvalidIdentityCookie
}
