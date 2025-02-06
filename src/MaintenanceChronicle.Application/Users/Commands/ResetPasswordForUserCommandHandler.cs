using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;

public class ResetPasswordForUserCommandHandler(UserManager<User> userManager) : IRequestHandler<ResetPasswordForUserCommand>
{
    public async Task Handle(ResetPasswordForUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.UserResetPasswordDto.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var result = await userManager
            .ResetPasswordAsync(user, request.UserResetPasswordDto.ResetToken, request.UserResetPasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            throw new BadRequestException(ErrorType.PasswordResetTokenInInvalid);
        }
    }
}
