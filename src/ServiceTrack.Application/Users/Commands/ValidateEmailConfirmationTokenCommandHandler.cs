using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data.Entities;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

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
