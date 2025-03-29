using MediatR;

namespace MaintenanceChronicle.Application.Contracts.RefreshTokens.Commands;
/// <summary>
/// Command to revoke a refresh token.
/// </summary>
/// <param name="Token">Token value to revoke</param>
public record RevokeRefreshTokenCommand(string Token) : IRequest;
