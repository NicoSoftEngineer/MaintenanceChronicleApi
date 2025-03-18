using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;

public class CheckUserLogInCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager) : IRequestHandler<CheckUserLogInCommand, SignInResult>
{
    public async Task<SignInResult> Handle(CheckUserLogInCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Login.Email);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Login.Password, false);
        return result;
    }
}
