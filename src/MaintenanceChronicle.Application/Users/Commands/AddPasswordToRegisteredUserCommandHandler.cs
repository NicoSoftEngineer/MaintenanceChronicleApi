using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;

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
