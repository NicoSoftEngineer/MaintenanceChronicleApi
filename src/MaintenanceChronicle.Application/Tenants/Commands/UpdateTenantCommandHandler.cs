using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Tenants.Commands;

public class UpdateTenantCommandHandler(AppDbContext dbContext,IClock clock) : IRequestHandler<UpdateTenantCommand>
{
    public async Task Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantEntity = await dbContext.Tenants.FindAsync(request.TenantDetail.Id, cancellationToken);
        if (tenantEntity == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }

        //map changed properties to entity
        tenantEntity.Name = request.TenantDetail.Name;

        tenantEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
