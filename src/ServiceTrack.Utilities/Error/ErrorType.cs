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
    InvalidIdentityCookie,
    [ErrorMessage("Sorry this role doesn't exist!")]
    RoleNotFound,
    [ErrorMessage("Sorry this customer doesn't exist!")]
    CustomerNotFound,
    [ErrorMessage("Sorry you don't have access to this tenant!")]
    UserNotInTenant
}
