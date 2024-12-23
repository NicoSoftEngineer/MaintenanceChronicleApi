using MaintenanceChronicle.Application.Contracts.Utils.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Machines.Commands;

public class DeleteMachineCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<DeleteEntityByIdCommand<Machine>>
{
    public async Task Handle(DeleteEntityByIdCommand<Machine> request, CancellationToken cancellationToken)
    {
        var machine = await dbContext.Machines.FindAsync(new object[] { request.Id }, cancellationToken);
        if (machine is null)
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }
        machine.SetDeleteBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
