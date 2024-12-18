using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;

namespace MaintenanceChronicle.Application.Locations.Queries;
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
