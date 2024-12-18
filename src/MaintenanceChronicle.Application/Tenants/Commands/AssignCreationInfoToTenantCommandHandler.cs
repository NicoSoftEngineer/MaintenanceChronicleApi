using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Tenants.Commands;

public class AssignCreationInfoToTenantCommandHandler(AppDbContext dbContext, IClock clock)
    : IRequestHandler<AssignCreationInfoToTenantCommand>
{
    public async Task Handle(AssignCreationInfoToTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await dbContext.Tenants.FirstAsync(x => x.Id == request.TenantsCreatorUserIdDto.TenantId, cancellationToken);
        tenant.SetCreateBy(request.TenantsCreatorUserIdDto.UserId, clock.GetCurrentInstant());
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
