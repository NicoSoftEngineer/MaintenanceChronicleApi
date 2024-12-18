using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Application.Contracts.Locations.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Data;

namespace ServiceTrack.Application.Locations.Queries;

public class GetListOfLocationsQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<LocationInListDto>, List<LocationInListDto>>
{
    public async Task<List<LocationInListDto>> Handle(GetListOfEntityQuery<LocationInListDto> request,
        CancellationToken cancellationToken)
    {
        var locationList = await dbContext.Locations.Select(x =>x.ToLocationInListDto()).ToListAsync(cancellationToken);

        return locationList;
    }
}
