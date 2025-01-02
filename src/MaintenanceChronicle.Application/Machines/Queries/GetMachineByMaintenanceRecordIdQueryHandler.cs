using MaintenanceChronicle.Application.Contracts.Machines.Queries;
using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Machines.Queries;

public class GetMachineByMaintenanceRecordIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetMachineByMaintenanceRecordIdQuery, MachineInMaintenanceRecordDetailDto>
{
    public async Task<MachineInMaintenanceRecordDetailDto> Handle(GetMachineByMaintenanceRecordIdQuery request,
        CancellationToken cancellationToken)
    {
        var record = await dbContext.MaintenanceRecords.Include(r => r.Machine).ThenInclude(m => m.Location)
            .FirstOrDefaultAsync(r => r.Id == request.RecordId, cancellationToken);
        if (record == null)
        {
            throw new BadRequestException(ErrorType.MaintenanceRecordNotFound);
        }

        var machineDto = record.ToMachineDto();

        return machineDto;
    }
}
