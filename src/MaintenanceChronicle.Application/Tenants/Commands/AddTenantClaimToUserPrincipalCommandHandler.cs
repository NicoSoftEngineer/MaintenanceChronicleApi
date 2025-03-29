using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Tenants.Commands;
/// <summary>
/// Handler for <see cref="AddTenantClaimsListCommand"/>
/// </summary>
public class AddTenantClaimToUserPrincipalCommandHandler(AppDbContext dbContext)
    : IRequestHandler<AddTenantClaimsListCommand, List<Claim>>
{
    public async Task<List<Claim>> Handle(AddTenantClaimsListCommand request, CancellationToken cancellationToken)
    {
        // Get user and tenant from database
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

        // Add claims to user
        request.Claims.Add(new Claim(MaintenanceChronicleClaimTypes.TenantIdClaimType, tenant.Id.ToString()));

        return request.Claims;
    }
}
