using System.Web;
using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.EmailTemplates;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Options;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace MaintenanceChronicle.Application.Users.Commands;
/// <summary>
/// Handler for <see cref="GenerateEmailConfirmationEmailForUserCommand"/>.
/// </summary>
public class GenerateEmailConfirmationEmailForUserCommandHandler(UserManager<User> userManager, IOptions<EnvironmentOptions> envOptions) : IRequestHandler<GenerateEmailConfirmationEmailForUserCommand, NewEmailMessageDto>
{
    public async Task<NewEmailMessageDto> Handle(GenerateEmailConfirmationEmailForUserCommand request,
        CancellationToken cancellationToken)
    {
        // Get user by email
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        // Generate confirmation link
        var confirmLink = $"{envOptions.Value.FrontendHostUrl}{envOptions.Value.FrontendConfirmUrl.Replace("[Email]", user.Email).Replace("[ConfToken]", request.Token)}";

        // Generate email body
        var emailHelper = new EmailTemplateHelper();
        var body = await emailHelper.GetEmailConfirmationTemplate($"{user.FirstName} {user.LastName}", confirmLink);

        // Create new email message
        var newEmailMessage = new NewEmailMessageDto
        {
            Subject = "Email confirmation",
            Body = body,
        };
        newEmailMessage.Recipients.Add(user.Email!, $"{user.FirstName} {user.LastName}");

        return newEmailMessage;
    }
}
