using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Application.Contracts.Tenants.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Data;

namespace ServiceTrack.Application.Tenants.Queries;

public class GetListOfTenantsQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<TenantListDto>, List<TenantListDto>>
{
    public async Task<List<TenantListDto>> Handle(GetListOfEntityQuery<TenantListDto> request,
        CancellationToken cancellationToken)
    {
        var tenants = await dbContext.Tenants.Select(t => new TenantListDto
        {
            Id = t.Id,
            Name = t.Name
        })
            .ToListAsync(cancellationToken);

        return tenants;
    }
}
