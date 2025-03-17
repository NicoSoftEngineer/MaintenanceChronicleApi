using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Utilities.Options;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MaintenanceChronicle.Application.Users.Commands;

public class GenerateAccessTokenFromClaimsCommandHandler(IOptions<JwtOptions> jwtOptions) : IRequestHandler<GenerateAccessTokenFromClaimsCommand, string>
{
    public async Task<string> Handle(GenerateAccessTokenFromClaimsCommand request, CancellationToken cancellationToken)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: request.Claims,
            expires: DateTime.Now.AddMinutes(jwtOptions.Value.AccessTokenExpirationInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
