using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Tenants.Commands;

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

        identity!.AddClaim(new(MaintenanceChronicleClaimTypes.TenantIdClaimType, tenant.Id.ToString()));

        return request.ClaimsPrincipal;
    }
}
