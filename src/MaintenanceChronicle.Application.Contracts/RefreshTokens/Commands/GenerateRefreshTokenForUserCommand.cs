using MediatR;

namespace MaintenanceChronicle.Application.Contracts.RefreshTokens.Commands;

/// <summary>
/// Generates a refresh token for user, for the user to be able to not log out after so little time
/// </summary>
/// <param name="UserEmail">User email for which the token should be generated</param>
/// <param name="RequestInfo">Additional info about the users request </param>
/// <returns>Hashed user refresh token</returns>
public record GenerateRefreshTokenForUserCommand(string UserEmail, string? RequestInfo = null) : IRequest<string>;
