using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Tenants.Commands;
/// <summary>
/// Handler for <see cref="UpdateTenantCommand"/>
/// </summary>
public class UpdateTenantCommandHandler(AppDbContext dbContext,IClock clock) : IRequestHandler<UpdateTenantCommand, TenantDetailDto>
{
    public async Task<TenantDetailDto> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantEntity = await dbContext.Tenants.FindAsync([request.Id], cancellationToken);
        if (tenantEntity == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }

        // Update the entity
        var dto = tenantEntity.ToDto();
        request.TenantDetail.ApplyTo(dto);
        dto.MapToEntity(tenantEntity);

        tenantEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
        return dto;
    }
}
