using System.Threading.Tasks.Dataflow;
using MaintenanceChronicle.Application.Contracts.Machines.Commands;
using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Machines.Commands;

public class CreateNewMachineCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewMachineCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewMachineCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Locations.FindAsync(new object[] { request.NewMachineDto.LocationId }, cancellationToken) ==
            null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        var machineEntity = request.NewMachineDto.ToMachineEntity();
        machineEntity.TenantId = Guid.Parse(request.TenantId);
        machineEntity.SetCreateBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.AddAsync(machineEntity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return machineEntity.Id;
    }
}
