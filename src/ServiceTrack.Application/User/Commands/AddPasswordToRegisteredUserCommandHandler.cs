using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceTrack.Application.Contracts.User.Commands;

namespace ServiceTrack.Application.User.Commands;

public class AddPasswordToRegisteredUserCommandHandler(UserManager<Data.Entities.User> userManager) : IRequestHandler<AddPasswordToRegisteredUserCommand,
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
