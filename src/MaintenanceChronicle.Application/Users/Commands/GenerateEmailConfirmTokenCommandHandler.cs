using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;

public class GenerateEmailConfirmTokenCommandHandler(UserManager<User> userManager) : IRequestHandler<GenerateEmailConfirmTokenCommand, string>
{
    public async Task<string> Handle(GenerateEmailConfirmTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var tokenEncoded = Uri.EscapeDataString(token);

        return tokenEncoded;
    }
}
