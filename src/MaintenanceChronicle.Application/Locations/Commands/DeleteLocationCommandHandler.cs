using MaintenanceChronicle.Application.Contracts.Utils.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Locations.Commands;

public class DeleteLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<DeleteEntityByIdCommand<Location>>
{
    public async Task Handle(DeleteEntityByIdCommand<Location> request, CancellationToken cancellationToken)
    {
        var location = await dbContext.Locations.FindAsync(request.Id,cancellationToken);
        if (location == null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        location.SetDeleteBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
