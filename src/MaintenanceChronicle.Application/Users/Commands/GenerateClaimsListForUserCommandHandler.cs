using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Commands;
/// <summary>
/// Handler for <see cref="GenerateClaimsListForUserCommand"/> command.
/// </summary>
public class GenerateClaimsListForUserCommandHandler(UserManager<User> userManager) : IRequestHandler<GenerateClaimsListForUserCommand, List<Claim>>
{
    public async Task<List<Claim>> Handle(GenerateClaimsListForUserCommand request, CancellationToken cancellationToken)
    {
        // Get user by email
        var user = await userManager.FindByEmailAsync(request.UserEmail);
        if (user == null) {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        //Assign claims with basic info
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString().ToLowerInvariant()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}")
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
