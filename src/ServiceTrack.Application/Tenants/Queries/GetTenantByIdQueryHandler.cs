using MediatR;
using ServiceTrack.Application.Contracts.Tenants.Queries;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Tenants.Queries;

public class GetTenantByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetEntityByIdQuery<TenantDto>, TenantDto>
{
    public async Task<TenantDto> Handle(GetEntityByIdQuery<TenantDto> request, CancellationToken cancellationToken)
    { 
        var tenantEntity = await dbContext.Tenants.FindAsync(request.Id);
        if (tenantEntity == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }
        var tenantDto = new TenantDto
        {
            Id = tenantEntity.Id,
            Name = tenantEntity.Name
        };
        return tenantDto;
    }
}
