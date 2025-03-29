using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;
/// <summary>
/// Handler for <see cref="GeneratePasswordResetTokenCommand"/> command.
/// </summary>
public class GeneratePasswordResetTokenCommandHandler(UserManager<User> userManager) : IRequestHandler<GeneratePasswordResetTokenCommand, string>
{
    public async Task<string> Handle(GeneratePasswordResetTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        // Generate password reset token for found user
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }
}
