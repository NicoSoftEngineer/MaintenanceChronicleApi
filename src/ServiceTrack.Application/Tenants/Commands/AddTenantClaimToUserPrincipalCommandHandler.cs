using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Application.Contracts.Tenants.Commands;
using ServiceTrack.Data;
using ServiceTrack.Utilities.Constants;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Tenants.Commands;

public class AddTenantClaimToUserPrincipalCommandHandler(AppDbContext dbContext)
    : IRequestHandler<AddTenantClaimToUserPrincipalCommand, ClaimsPrincipal>
{
    public async Task<ClaimsPrincipal> Handle(AddTenantClaimToUserPrincipalCommand request, CancellationToken cancellationToken)
    {
        var userTenantClaim = request.UserTenantClaim;
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == userTenantClaim.Email, cancellationToken);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var tenant = await dbContext.Tenants.FirstOrDefaultAsync(x => x.Id == userTenantClaim.TenantId, cancellationToken);
        if (tenant == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }

        var identity = request.ClaimsPrincipal.Identity as ClaimsIdentity;
        if (tenant == null)
        {
            throw new BadRequestException(ErrorType.InvalidIdentityCookie);
        }

        identity!.AddClaim(new(ServiceTrackClaimTypes.TenantIdClaimType, tenant.Id.ToString()));

        return request.ClaimsPrincipal;
    }
}
