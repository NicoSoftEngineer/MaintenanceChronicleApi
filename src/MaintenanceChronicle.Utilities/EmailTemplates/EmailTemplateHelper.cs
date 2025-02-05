namespace MaintenanceChronicle.Utilities.EmailTemplates;

public class EmailTemplateHelper
{
    /// <summary>
    /// Gets email confirm template from file system, and assign userName and confirmationLink
    /// </summary>
    /// <param name="userName">User name</param>
    /// <param name="confirmationLink">Email confirmation link for user</param>
    /// <returns></returns>
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
    /// <returns></returns>
    public async Task<string> GetPasswordResetEmailTemplate(string userName, string passwordResetLink)
    {
        var template = await File.ReadAllTextAsync("../MaintenanceChronicle.Utilities/EmailTemplates/PasswordReset.html");
        template = template.Replace("[UserName]", userName).Replace("[ResetLink]", passwordResetLink);

        return template;
    }
}
