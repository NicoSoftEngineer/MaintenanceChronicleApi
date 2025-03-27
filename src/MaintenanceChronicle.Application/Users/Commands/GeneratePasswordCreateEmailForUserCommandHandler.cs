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

public class GeneratePasswordCreateEmailForUserCommandHandler(UserManager<User> userManager, IOptions<EnvironmentOptions> envOptions) : IRequestHandler<GeneratePasswordCreateEmailForUserCommand, NewEmailMessageDto>
{
    public async Task<NewEmailMessageDto> Handle(GeneratePasswordCreateEmailForUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var passwordResetLink = $"{envOptions.Value.FrontendHostUrl}{envOptions.Value.FrontendPasswordCreateUrl.Replace("[Email]", user.Email).Replace("[PasswordToken]", request.PasswordToken).Replace("[ConfToken]", request.ConfToken)}";

        var emailHelper = new EmailTemplateHelper();
        var body = await emailHelper.GetUserInvitationEmailTemplate($"{user.FirstName} {user.LastName}", passwordResetLink);

        var newEmailMessage = new NewEmailMessageDto
        {
            Subject = "Email confirmation",
            Body = body,
        };
        newEmailMessage.Recipients.Add((user.Email!, $"{user.FirstName} {user.LastName}"));

        return newEmailMessage;
    }
}
