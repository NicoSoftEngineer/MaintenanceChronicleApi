using MaintenanceChronicle.Application.Contracts.Locations.Commands;
using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Locations.Commands;
/// <summary>
/// Handler for <see cref="UpdateLocationCommand"/>
/// </summary>
public class UpdateLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateLocationCommand, ManageLocationDetailDto>
{
    public async Task<ManageLocationDetailDto> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        // Get current location from db
        var locationEntity = await dbContext.Locations.FindAsync([request.LocationId], cancellationToken);
        if (locationEntity == null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }
        var patch = request.Patch;
        // map entity to dto
        var locationMapped = locationEntity.ToManageDto();
        // apply patch
        patch.ApplyTo(locationMapped);
        // map changed props back to entity
        locationMapped.MapToEntity(locationEntity);
        locationEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        // save changes
        await dbContext.SaveChangesAsync(cancellationToken);

        return locationMapped;
    }
}
