using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Utilities.Options;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MaintenanceChronicle.Application.Users.Commands;

/// <summary>
/// Handler for <see cref="GenerateAccessTokenFromClaimsCommand"/>
/// </summary>
public class GenerateAccessTokenFromClaimsCommandHandler(IOptions<JwtOptions> jwtOptions) : IRequestHandler<GenerateAccessTokenFromClaimsCommand, string>
{
    public Task<string> Handle(GenerateAccessTokenFromClaimsCommand request, CancellationToken cancellationToken)
    {
        // create key, by which to sign and verify the token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

        // create credentials using created key and SHA256 algorithm
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // create token with issuer, audience, claims, expiration and signing credentials
        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: request.Claims,
            expires: DateTime.Now.AddMinutes(jwtOptions.Value.AccessTokenExpirationInMinutes),
            signingCredentials: creds);

        // builds the token from the token object, encodes it in base64 and returns it
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
