namespace MaintenanceChronicle.Utilities.EmailTemplates;
/// <summary>
/// Helper class to get email templates from file system and fill out the placeholders.
/// </summary>
public class EmailTemplateHelper
{
    /// <summary>
    /// Gets email confirm template from file system, and assign userName and confirmationLink
    /// </summary>
    /// <param name="userName">User name</param>
    /// <param name="confirmationLink">Email confirmation link for user</param>
    /// <returns>Email template</returns>
    public async Task<string> GetEmailConfirmationTemplate(string userName, string confirmationLink)
    {
        var template = await File.ReadAllTextAsync("../MaintenanceChronicle.Utilities/EmailTemplates/EmailConfirmation.html");
        template = template.Replace("[UserName]", userName).Replace("[ConfirmationLink]", confirmationLink);

        return template;
    }

    /// <summary>
    /// Gets password reset template from file system, and assign userName and passwordResetLink
    /// </summary>
    /// <param name="userName">User name</param>
    /// <param name="passwordResetLink">Password reset link for user</param>
    /// <returns>Email template</returns>
    public async Task<string> GetPasswordResetEmailTemplate(string userName, string passwordResetLink)
    {
        var template = await File.ReadAllTextAsync("../MaintenanceChronicle.Utilities/EmailTemplates/PasswordReset.html");
        template = template.Replace("[UserName]", userName).Replace("[ResetLink]", passwordResetLink);

        return template;
    }

    /// <summary>
    /// Gets UserInvitationEmailTemplate from file system, fills out userName and link into email
    /// </summary>
    /// <param name="userName">User name</param>
    /// <param name="link">Create password and confirm email link</param>
    /// <returns>Email template</returns>
    public async Task<string> GetUserInvitationEmailTemplate(string userName, string link)
    {
        var template = await File.ReadAllTextAsync("../MaintenanceChronicle.Utilities/EmailTemplates/UserInvitationEmail.html");
        template = template.Replace("[UserName]", userName).Replace("[Link]", link);

        return template;
    }
    /// <summary>
    /// Gets MaintenanceReminderEmailTemplate from file system, fills out machineName, machineSerialNumber, date, description, locationName, locationAddress into email
    /// </summary>
    /// <param name="machineName">Machine name</param>
    /// <param name="machineSerialNumber">Machine serial number</param>
    /// <param name="date">Date when the reminder should be performed</param>
    /// <param name="description">Description of what should be done</param>
    /// <param name="locationName">Location name</param>
    /// <param name="locationAddress">Locations address</param>
    /// <returns>Email template</returns>
    public async Task<string> GetMaintenanceReminderEmailTemplate(string machineName, string machineSerialNumber,
        string date, string description, string locationName, string locationAddress)
    {
        var template = await File.ReadAllTextAsync("../MaintenanceChronicle.Utilities/EmailTemplates/MaintenanceReminderEmail.html");
        template = template
            .Replace("[MachineName]", machineName)
            .Replace("[MachineSerialNumber]", machineSerialNumber)
            .Replace("[Date]", date)
            .Replace("[Description]", description)
            .Replace("[LocationName]", locationName)
            .Replace("[LocationAddress]", locationAddress);
        return template;
    }
}
