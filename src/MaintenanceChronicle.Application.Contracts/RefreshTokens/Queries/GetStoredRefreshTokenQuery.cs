using MaintenanceChronicle.Application.Contracts.RefreshTokens.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.RefreshTokens.Queries;

public record GetStoredRefreshTokenQuery(string IncomingRefreshToken) : IRequest<RefreshTokenDto?>;
