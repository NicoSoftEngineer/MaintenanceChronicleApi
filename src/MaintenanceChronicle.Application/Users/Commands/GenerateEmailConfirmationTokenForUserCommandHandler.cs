using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;

public class GenerateEmailConfirmationTokenForUserCommandHandler(UserManager<User> userManager) : IRequestHandler<GenerateEmailConfirmationTokenForUserCommand, string>
{
    public async Task<string> Handle(GenerateEmailConfirmationTokenForUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }
}
