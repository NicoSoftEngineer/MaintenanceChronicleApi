using MaintenanceChronicle.Application.Contracts.RefreshTokens.Queries;
using MaintenanceChronicle.Application.Contracts.RefreshTokens.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.RefreshTokens.Queries;
/// <summary>
/// Handler for <see cref="GetStoredRefreshTokenQuery"/>.
/// </summary>
public class GetStoredRefreshTokenQueryHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<GetStoredRefreshTokenQuery, RefreshTokenDto?>
{
    public async Task<RefreshTokenDto?> Handle(GetStoredRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        // Check if the token is valid and exists
        var now = clock.GetCurrentInstant();
        var storedToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == request.IncomingRefreshToken.Hash(), cancellationToken: cancellationToken);
        if (storedToken == null || storedToken.ExpiresAt < now || storedToken.RevokedAt != null)
        {
            return null;
        }

        // returns only valid token
        return storedToken.ToDto();
    }
}
