using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data.Entities.Account;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

public class GenerateClaimsPrincipalForUserCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager) : IRequestHandler<GenerateClaimsPrincipalForUserCommand, ClaimsPrincipal>
{
    public async Task<ClaimsPrincipal> Handle(GenerateClaimsPrincipalForUserCommand request, CancellationToken cancellationToken)
    {
        var userLogin = request.UserLogin;

        var user = await userManager.FindByEmailAsync(userLogin.Email);
        if (user == null) {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, userLogin.Password, lockoutOnFailure: false);
        if (!signInResult.Succeeded)
        {
            throw new BadRequestException(ErrorType.InvalidPassword);
        }

        return await signInManager.CreateUserPrincipalAsync(user);
    }
}
