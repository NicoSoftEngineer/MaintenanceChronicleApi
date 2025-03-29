using MaintenanceChronicle.Application.Contracts.Tenants.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;

namespace MaintenanceChronicle.Application.Tenants.Queries;
/// <summary>
/// Handler for <see cref="GetEntityByIdQuery{TEntity}"/> for <see cref="TenantDetailDto"/>.
/// </summary>
public class GetTenantByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetEntityByIdQuery<TenantDetailDto>, TenantDetailDto>
{
    public async Task<TenantDetailDto> Handle(GetEntityByIdQuery<TenantDetailDto> request, CancellationToken cancellationToken)
    { 
        var tenantEntity = await dbContext.Tenants.FindAsync([request.Id], cancellationToken);
        if (tenantEntity == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }
        var tenantDto = new TenantDetailDto
        {
            Id = tenantEntity.Id,
            Name = tenantEntity.Name
        };
        return tenantDto;
    }
}
