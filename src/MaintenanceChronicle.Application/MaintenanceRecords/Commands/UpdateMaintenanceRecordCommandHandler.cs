using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands;
using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NodaTime;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Commands;

public class UpdateMaintenanceRecordCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateMaintenanceRecordCommand, ManageMaintenanceRecordDetailDto>
{
    public async Task<ManageMaintenanceRecordDetailDto> Handle(UpdateMaintenanceRecordCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.MaintenanceRecords.FindAsync([request.Id], cancellationToken);
        if (entity == null)
        {
            throw new BadRequestException(ErrorType.MaintenanceRecordNotFound);
        }

        var entityMapped = entity.ToManageDto();
        request.Patch.ApplyTo(entityMapped);

        if (!await dbContext.Machines.AnyAsync(m => m.Id == entityMapped.MachineId, cancellationToken: cancellationToken))
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        entityMapped.MapToEntity(entity);
        entity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.ToManageDto();
    }
}
