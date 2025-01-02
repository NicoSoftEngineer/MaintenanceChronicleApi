using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries;
using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Queries;

public class GetMaintenanceRecordsByMachineIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetMaintenanceRecordsByMachineIdQuery, List<MaintenanceRecordInListForMachineDto>>
{
    public async Task<List<MaintenanceRecordInListForMachineDto>> Handle(GetMaintenanceRecordsByMachineIdQuery request,
        CancellationToken cancellationToken)
    {
        var machine = await dbContext.Machines.Include(m => m.MaintenanceRecords)
            .FirstOrDefaultAsync(m => m.Id == request.MachineId, cancellationToken: cancellationToken);

        if (machine == null)
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        var records = machine.MaintenanceRecords.Select(m => m.ToListForMachineDto()).ToList();

        return records;
    }
}
