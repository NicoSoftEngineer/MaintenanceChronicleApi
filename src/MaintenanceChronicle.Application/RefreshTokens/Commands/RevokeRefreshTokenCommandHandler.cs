using MaintenanceChronicle.Application.Contracts.RefreshTokens.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.RefreshTokens.Commands;

public class RevokeRefreshTokenCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<RevokeRefreshTokenCommand>
{
    public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var now = clock.GetCurrentInstant();
        var storedToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == request.Token.Hash(), cancellationToken: cancellationToken);
        if (storedToken == null)
        {
            throw new BadRequestException(ErrorType.TokenNotFound);
        }
        storedToken.RevokedAt = now;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
