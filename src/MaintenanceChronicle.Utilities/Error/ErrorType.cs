
namespace MaintenanceChronicle.Utilities.Error;

//TODO: localize
/// <summary>
/// Enum for error types.
/// </summary>
public enum ErrorType
{
    [ErrorMessage("Uživatel s tímto e-mailem již existuje!")]
    EmailAlreadyExists,
    [ErrorMessage("Omlouváme se, tento uživatel neexistuje!")]
    UserNotFound,
    [ErrorMessage("Omlouváme se, toto heslo nesplňuje naše požadavky!")]
    PasswordDoesNotMeetRequirements,
    [ErrorMessage("Špatné heslo!")]
    InvalidPassword,
    [ErrorMessage("Omlouváme se, tento token je neplatný!")]
    InvalidEmailConfirmationToken,
    [ErrorMessage("Název musí být jedinečný!")]
    NameMustBeUnique,
    [ErrorMessage("Omlouváme se, tento nájemce neexistuje!")]
    TenantNotFound,
    [ErrorMessage("Omlouváme se, vaši identitu se nepodařilo ověřit!")]
    InvalidIdentityCookie,
    [ErrorMessage("Omlouváme se, tato role neexistuje!")]
    RoleNotFound,
    [ErrorMessage("Omlouváme se, tento zákazník neexistuje!")]
    CustomerNotFound,
    [ErrorMessage("Omlouváme se, nemáte přístup k tomuto nájemci!")]
    UserNotInTenant,
    [ErrorMessage("Omlouváme se, tato lokalita neexistuje!")]
    LocationNotFound,
    [ErrorMessage("Omlouváme se, tento stroj neexistuje!")]
    MachineNotFound,
    [ErrorMessage("Omlouváme se, tento záznam údržby neexistuje!")]
    MaintenanceRecordNotFound,
    [ErrorMessage("Omlouváme se, tato e-mailová zpráva neexistuje!")]
    EmailMessageNotFound,
    [ErrorMessage("Omlouváme se, tato e-mailová zpráva již byla odeslána!")]
    EmailAlreadySent,
    [ErrorMessage("Omlouváme se, tento token pro reset hesla je neplatný!")]
    PasswordResetTokenInInvalid,
    [ErrorMessage("Omlouváme se, nejste přihlášený")]
    UserNotLoggedIn,
    [ErrorMessage("Omlouváme se, tento už má heslo!")]
    UserAlreadyHasPassword,
    [ErrorMessage("Neplatný obnovovací token nebo jeho platnost vypršela!")]
    InvalidRefreshToken,
    [ErrorMessage("Obnovovací token nebyl nalezen!")]
    TokenNotFound,
    [ErrorMessage("Špatné přihlašovací údaje!")]
    InvalidLogIn,
    [ErrorMessage("Omlouváme se, toto upozorňení neexistuje!")]
    MaintenanceReminderNotFound,
    [ErrorMessage("Omlouváme se, id uživatele nebylo nalezeno!")]
    UserIdNotFound,
    [ErrorMessage("Omlouváme se, id organizace nebylo nalezeno!")]
    TenantIdNotFound
}
