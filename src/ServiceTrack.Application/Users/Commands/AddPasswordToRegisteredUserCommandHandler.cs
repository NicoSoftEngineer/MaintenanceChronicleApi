using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data.Entities;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

public class AddPasswordToRegisteredUserCommandHandler(UserManager<User> userManager) : IRequestHandler<AddPasswordToRegisteredUserCommand>
{
    public async Task Handle(AddPasswordToRegisteredUserCommand request,
        CancellationToken cancellationToken)
    {
        var userPasswordDto = request.UserPasswordDto;

        var user = await userManager.FindByEmailAsync(userPasswordDto.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var result = await userManager.AddPasswordAsync(user, userPasswordDto.Password);
        if (!result.Succeeded)
        {
            throw new BadRequestException(ErrorType.PasswordDoesNotMeetRequirements);
        }
    }
}
