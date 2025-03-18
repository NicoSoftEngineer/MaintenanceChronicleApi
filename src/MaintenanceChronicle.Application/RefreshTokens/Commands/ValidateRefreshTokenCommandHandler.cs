using MaintenanceChronicle.Application.Contracts.RefreshTokens.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.RefreshTokens.Commands;

public class ValidateRefreshTokenCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<ValidateRefreshTokenCommand, bool>
{
    public async Task<bool> Handle(ValidateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var now = clock.GetCurrentInstant();
        var storedToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == request.IncomingRefreshToken.Hash(), cancellationToken: cancellationToken);
        if (storedToken == null || storedToken.ExpiresAt < now || storedToken.RevokedAt != null)
        {
            return false;
        }

        return true;
    }
}
