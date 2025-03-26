using System.Security.Claims;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

/// <summary>
/// Generates JWT access token for user using specified claims
/// </summary>
/// <param name="Claims">Claims from which the token should be generated</param>
/// <returns>Generated JWT toke</returns>
public record GenerateAccessTokenFromClaimsCommand(List<Claim> Claims) : IRequest<string>;
