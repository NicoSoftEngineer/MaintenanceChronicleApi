using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands;
using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Commands;

public class CreateNewMaintenanceRecordCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewMaintenanceRecordCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewMaintenanceRecordCommand request, CancellationToken cancellationToken)
    {
        if (!await dbContext.Machines.AnyAsync(x => x.Id == request.RecordDto.MachineId, cancellationToken))
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        var recordEntity = request.RecordDto.ToEntity();
        recordEntity.TenantId = Guid.Parse(request.TenantId);
        recordEntity.SetCreateBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.AddAsync(recordEntity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        return recordEntity.Id;
    }
}
