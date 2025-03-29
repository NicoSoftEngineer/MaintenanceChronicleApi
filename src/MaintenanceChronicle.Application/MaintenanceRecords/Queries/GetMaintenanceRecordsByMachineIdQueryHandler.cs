using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries;
using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Queries;
/// <summary>
/// Handler for <see cref="GetMaintenanceRecordsByMachineIdQuery"/>.
/// </summary>
public class GetMaintenanceRecordsByMachineIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetMaintenanceRecordsByMachineIdQuery, List<MaintenanceRecordInListForMachineDto>>
{
    public async Task<List<MaintenanceRecordInListForMachineDto>> Handle(GetMaintenanceRecordsByMachineIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get machine with maintenance records
        var machine = await dbContext.Machines.Include(m => m.MaintenanceRecords)
            .FirstOrDefaultAsync(m => m.Id == request.MachineId, cancellationToken: cancellationToken);
        if (machine == null)
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }
        // Convert maintenance records to DTO
        var records = machine.MaintenanceRecords.Select(m => m.ToListForMachineDto()).ToList();

        return records;
    }
}
