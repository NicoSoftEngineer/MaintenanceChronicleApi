using MediatR;

namespace MaintenanceChronicle.Application.Contracts.RefreshTokens.Commands;

public record RevokeRefreshTokenCommand(string Token) : IRequest;
