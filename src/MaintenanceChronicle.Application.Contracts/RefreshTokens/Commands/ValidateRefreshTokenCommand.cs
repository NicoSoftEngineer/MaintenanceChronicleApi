using MediatR;

namespace MaintenanceChronicle.Application.Contracts.RefreshTokens.Commands;

public record ValidateRefreshTokenCommand(string IncomingRefreshToken) : IRequest<bool>;
