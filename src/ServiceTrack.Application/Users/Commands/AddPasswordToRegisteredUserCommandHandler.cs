using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data.Entities;

namespace ServiceTrack.Application.Users.Commands;

public class AddPasswordToRegisteredUserCommandHandler(UserManager<User> userManager) : IRequestHandler<AddPasswordToRegisteredUserCommand,
                                                                         AddPasswordToRegisteredUserCommandResult>
{
    public async Task<AddPasswordToRegisteredUserCommandResult> Handle(AddPasswordToRegisteredUserCommand request,
        CancellationToken cancellationToken)
    {
        var userPasswordDto = request.UserPasswordDto;

        var user = await userManager.FindByEmailAsync(userPasswordDto.Email);
        if (user == null)
        {
            return AddPasswordToRegisteredUserCommandResult.UserNotFound;
        }

        var result = await userManager.AddPasswordAsync(user, userPasswordDto.Password);
        if (!result.Succeeded)
        {
            return AddPasswordToRegisteredUserCommandResult.PasswordDoesNotMeetRequirements;
        }

        return AddPasswordToRegisteredUserCommandResult.Success;
    }

}
