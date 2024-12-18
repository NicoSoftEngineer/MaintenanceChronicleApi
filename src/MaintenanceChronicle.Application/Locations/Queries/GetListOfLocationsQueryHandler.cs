using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Locations.Queries;

public class GetListOfLocationsQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<LocationInListDto>, List<LocationInListDto>>
{
    public async Task<List<LocationInListDto>> Handle(GetListOfEntityQuery<LocationInListDto> request,
        CancellationToken cancellationToken)
    {
        var locationList = await dbContext.Locations.Select(x =>x.ToLocationInListDto()).ToListAsync(cancellationToken);

        return locationList;
    }
}
