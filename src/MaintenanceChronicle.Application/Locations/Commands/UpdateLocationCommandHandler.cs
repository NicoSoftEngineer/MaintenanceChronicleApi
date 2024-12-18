using MaintenanceChronicle.Application.Contracts.Locations.Commands;
using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Locations.Commands;

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
