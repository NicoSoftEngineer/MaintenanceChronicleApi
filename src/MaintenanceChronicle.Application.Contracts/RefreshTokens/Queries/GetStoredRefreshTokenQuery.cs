using MaintenanceChronicle.Application.Contracts.RefreshTokens.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.RefreshTokens.Queries;
/// <summary>
/// Query to get stored refresh token if the token exists or is not expired.
/// </summary>
/// <param name="IncomingRefreshToken">Value of incoming refresh token to query by</param>
/// <returns>StoredRefreshTokenDto</returns>
public record GetStoredRefreshTokenQuery(string IncomingRefreshToken) : IRequest<RefreshTokenDto?>;
