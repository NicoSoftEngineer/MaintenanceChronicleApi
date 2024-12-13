using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ServiceTrack.Application.Contracts.Locations.Commands;
using ServiceTrack.Application.Contracts.Locations.Commands.Dto;
using ServiceTrack.Data;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Locations.Commands;

public class UpdateLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateLocationCommand, ManageLocationDetailDto>
{
    public async Task<ManageLocationDetailDto> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var locationEntity = await dbContext.Locations.FindAsync(request.LocationId, cancellationToken);
        if (locationEntity == null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }
        var patch = request.Patch;

        var locationMapped = locationEntity.ToManageDto();
        patch.ApplyTo(locationMapped);

        locationMapped.MapToEntity(locationEntity);
        locationEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);

        return locationMapped;
    }
}
