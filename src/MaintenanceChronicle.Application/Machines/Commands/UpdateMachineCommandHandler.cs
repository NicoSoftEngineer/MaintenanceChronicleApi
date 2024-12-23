using MaintenanceChronicle.Application.Contracts.Machines.Commands;
using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Machines.Commands;

public class UpdateMachineCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateMachineCommand,ManageMachineDetailDto>
{
    public async Task<ManageMachineDetailDto> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
    {
        var machineEntity = await dbContext.Machines.FindAsync(new object[] { request.MachineId }, cancellationToken);
        if (machineEntity is null)
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        var machineDetail = machineEntity.ToManageMachineDetailDto();

        request.Patch.ApplyTo(machineDetail);

        if (!(await dbContext
                .Locations
                .AnyAsync(l => l.Id == machineDetail.LocationId, cancellationToken)))
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        machineDetail.MapToEntity(machineEntity);
        machineEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);

        return machineDetail;
    }
}
