using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;
/// <summary>
/// Handler for <see cref="ResetPasswordForUserCommand"/>.
/// </summary>
public class ResetPasswordForUserCommandHandler(UserManager<User> userManager) : IRequestHandler<ResetPasswordForUserCommand>
{
    public async Task Handle(ResetPasswordForUserCommand request, CancellationToken cancellationToken)
    {
        // get user by email
        var user = await userManager.FindByEmailAsync(request.UserResetPasswordDto.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        // reset password if reset token is valid
        var result = await userManager
            .ResetPasswordAsync(user, request.UserResetPasswordDto.ResetToken, request.UserResetPasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            throw new BadRequestException(ErrorType.PasswordResetTokenInInvalid);
        }
    }
}
