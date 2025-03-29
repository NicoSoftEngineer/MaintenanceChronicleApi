using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MimeKit.Text;

namespace MaintenanceChronicle.Application.Users.Commands;
/// <summary>
/// Handler for <see cref="ValidateEmailConfirmationTokenCommand"/>
/// </summary>
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
        // Decode the token
        var decodedToken = Uri.UnescapeDataString(userTokenDto.Token);

        // Check if the token is valid for user
        var tokenCheckResult = await userManager.ConfirmEmailAsync(user, decodedToken);
        if (!tokenCheckResult.Succeeded)
        {
            throw new BadRequestException(ErrorType.InvalidEmailConfirmationToken);
        }
    }
}
