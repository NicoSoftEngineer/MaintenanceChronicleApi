namespace MaintenanceChronicle.Utilities.EmailTemplates;

public class EmailTemplateHelper
{
    public async Task<string> GetEmailConfirmationTemplate(string userName, string confirmationLink)
    {
        var template = await File.ReadAllTextAsync("../MaintenanceChronicle.Utilities/EmailTemplates/EmailConfirmation.html");
        template = template.Replace("[UserName]", userName).Replace("[ConfirmationLink]", confirmationLink);

        return template;
    }
}
