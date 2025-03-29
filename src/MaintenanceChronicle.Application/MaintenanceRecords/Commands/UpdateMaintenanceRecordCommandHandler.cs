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
/// <summary>
/// Handler for <see cref="UpdateMaintenanceRecordCommand"/>
/// </summary>
public class UpdateMaintenanceRecordCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateMaintenanceRecordCommand, ManageMaintenanceRecordDetailDto>
{
    public async Task<ManageMaintenanceRecordDetailDto> Handle(UpdateMaintenanceRecordCommand request,
        CancellationToken cancellationToken)
    {
        // Check if the record exists
        var entity = await dbContext.MaintenanceRecords.FindAsync([request.Id], cancellationToken);
        if (entity == null)
        {
            throw new BadRequestException(ErrorType.MaintenanceRecordNotFound);
        }

        // Apply the patch to the entity
        var entityMapped = entity.ToManageDto();
        request.Patch.ApplyTo(entityMapped);
        // Check if the machine exists
        // Need to check here, because the patch does not show if the machine exists
        if (!await dbContext.Machines.AnyAsync(m => m.Id == entityMapped.MachineId, cancellationToken: cancellationToken))
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }
        // Map the entity back to the entity
        entityMapped.MapToEntity(entity);
        entity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.ToManageDto();
    }
}
