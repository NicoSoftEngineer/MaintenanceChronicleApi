using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data.Entities;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

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
