using MaintenanceChronicle.Application.Contracts.RefreshTokens.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Helpers;
using MaintenanceChronicle.Utilities.Options;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NodaTime;

namespace MaintenanceChronicle.Application.RefreshTokens.Commands;

public class GenerateRefreshTokenForUserCommandHandler(AppDbContext dbContext, UserManager<User> userManager, IClock clock, IOptions<JwtOptions> options) : IRequestHandler<GenerateRefreshTokenForUserCommand, string>
{
    public async Task<string> Handle(GenerateRefreshTokenForUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.UserEmail);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }
        var refreshToken = Guid.NewGuid().ToString();

        var now = clock.GetCurrentInstant();
        dbContext.Add(new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken.Hash(),
            CreatedAt = now,
            ExpiresAt = now.Plus(Duration.FromDays(options.Value.RefreshTokenExpirationInDays)),
            RequestInfo = request.RequestInfo,
        });
        await dbContext.SaveChangesAsync(cancellationToken);
        return refreshToken;
    }
}
