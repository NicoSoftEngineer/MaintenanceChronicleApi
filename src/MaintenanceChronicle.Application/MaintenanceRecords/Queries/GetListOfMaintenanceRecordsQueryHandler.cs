using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Queries;

public class GetListOfMaintenanceRecordsQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<MaintenanceRecordInListDto>, List<MaintenanceRecordInListDto>>
{
    public async Task<List<MaintenanceRecordInListDto>> Handle(GetListOfEntityQuery<MaintenanceRecordInListDto> request, CancellationToken cancellationToken)
    {
        var records = await dbContext.MaintenanceRecords.Include(m => m.Machine).ThenInclude(m => m.Location)
            .Select(m => m.ToListDto()).ToListAsync(cancellationToken);

        return records;
    }
}
