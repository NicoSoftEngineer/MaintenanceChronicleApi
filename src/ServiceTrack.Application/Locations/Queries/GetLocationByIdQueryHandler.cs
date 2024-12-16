using MediatR;
using ServiceTrack.Application.Contracts.Locations.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Data;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Locations.Queries;
public class GetLocationByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetEntityByIdQuery<LocationDetailDto>, LocationDetailDto>
{
    public async Task<LocationDetailDto> Handle(GetEntityByIdQuery<LocationDetailDto> request,
        CancellationToken cancellationToken)
    {
        var location = await dbContext.Locations.FindAsync(request.Id, cancellationToken);
        if (location == null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        var locationDetailDto = location.ToLocationDetailDto();

        return locationDetailDto;
    }
}
