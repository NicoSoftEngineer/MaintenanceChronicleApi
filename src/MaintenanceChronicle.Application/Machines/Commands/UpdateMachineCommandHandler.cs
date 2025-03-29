using MaintenanceChronicle.Application.Contracts.Machines.Commands;
using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Machines.Commands;
/// <summary>
/// Handler for <see cref="UpdateMachineCommand"/>
/// </summary>
public class UpdateMachineCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateMachineCommand,ManageMachineDetailDto>
{
    public async Task<ManageMachineDetailDto> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
    {
        // Get machine from db
        var machineEntity = await dbContext.Machines.FindAsync(new object[] { request.MachineId }, cancellationToken);
        if (machineEntity is null)
        {
           throw new BadRequestException(ErrorType.MachineNotFound);
        }
        // Map to dto
        var machineDetail = machineEntity.ToManageMachineDetailDto();
        // Apply patch to dto
        request.Patch.ApplyTo(machineDetail);

        if (!(await dbContext
                .Locations
                .AnyAsync(l => l.Id == machineDetail.LocationId, cancellationToken)))
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }
        // Map changed properties back to entity
        machineDetail.MapToEntity(machineEntity);
        machineEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);

        return machineDetail;
    }
}
