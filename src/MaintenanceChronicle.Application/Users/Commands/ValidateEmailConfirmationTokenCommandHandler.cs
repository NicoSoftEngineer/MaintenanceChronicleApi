using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;

public class ValidateEmailConfirmationTokenCommandHandler(UserManager<User> userManager) : IRequestHandler<ValidateEmailConfirmationTokenCommand>
{
    public async Task Handle(ValidateEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
    {
        var userTokenDto = request.EmailConfirmTokenForUserDto;

        var user = await userManager.FindByEmailAsync(userTokenDto.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var tokenCheckResult = await userManager.ConfirmEmailAsync(user, userTokenDto.Token);
        if (!tokenCheckResult.Succeeded)
        {
            throw new BadRequestException(ErrorType.InvalidEmailConfirmationToken);
        }
    }
}
