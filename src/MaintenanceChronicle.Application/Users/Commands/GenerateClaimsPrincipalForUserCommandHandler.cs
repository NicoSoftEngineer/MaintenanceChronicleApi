using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;

public class GenerateClaimsListForUserCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager) : IRequestHandler<GenerateClaimsListForUserCommand, List<Claim>>
{
    public async Task<List<Claim>> Handle(GenerateClaimsListForUserCommand request, CancellationToken cancellationToken)
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

        //Claims with basic info
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString().ToLowerInvariant()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, user.UserName!)
        };

        //Adding user roles to claims
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));   
        }

        return claims;
    }
}
