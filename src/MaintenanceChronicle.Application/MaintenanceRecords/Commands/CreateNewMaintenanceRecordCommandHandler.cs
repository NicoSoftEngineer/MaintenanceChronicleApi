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
/// <summary>
/// Handler for <see cref="CreateNewMaintenanceRecordCommand"/>
/// </summary>
public class CreateNewMaintenanceRecordCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewMaintenanceRecordCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewMaintenanceRecordCommand request, CancellationToken cancellationToken)
    {
        // Check if machine exists
        if (!await dbContext.Machines.AnyAsync(x => x.Id == request.RecordDto.MachineId, cancellationToken))
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        // Map dto to entity
        var recordEntity = request.RecordDto.ToEntity();
        recordEntity.TenantId = Guid.Parse(request.TenantId);
        recordEntity.SetCreateBy(request.UserId, clock.GetCurrentInstant());
        // add entity to db
        await dbContext.AddAsync(recordEntity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        return recordEntity.Id;
    }
}
