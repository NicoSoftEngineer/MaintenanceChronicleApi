using MaintenanceChronicle.Application.Contracts.RefreshTokens.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.RefreshTokens.Commands;
/// <summary>
/// Handler for <see cref="RevokeRefreshTokenCommand"/>
/// </summary>
public class RevokeRefreshTokenCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<RevokeRefreshTokenCommand>
{
    public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Get current time
        var now = clock.GetCurrentInstant();
        // get the token from the database
        var storedToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == request.Token.Hash(), cancellationToken: cancellationToken);
        if (storedToken == null)
        {
            throw new BadRequestException(ErrorType.TokenNotFound);
        }
        // Revoke the token
        storedToken.RevokedAt = now;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
