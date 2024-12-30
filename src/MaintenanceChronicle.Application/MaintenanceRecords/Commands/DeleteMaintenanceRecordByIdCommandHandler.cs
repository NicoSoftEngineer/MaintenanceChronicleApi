using MaintenanceChronicle.Application.Contracts.Utils.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Commands;

public class DeleteMaintenanceRecordByIdCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<DeleteEntityByIdCommand<MaintenanceRecord>>
{
    public async Task Handle(DeleteEntityByIdCommand<MaintenanceRecord> request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.MaintenanceRecords.FindAsync([request.Id], cancellationToken);
        if (entity == null)
        {
            throw new BadRequestException(ErrorType.MaintenanceRecordNotFound);
        }

        entity.SetDeleteBy(request.UserId, clock.GetCurrentInstant());
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
