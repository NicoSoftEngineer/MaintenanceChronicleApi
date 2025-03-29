using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Queries;
/// <summary>
/// Handler for <see cref="GetListOfEntityQuery{MaintenanceRecordInListDto}"/> to get list of <see cref="MaintenanceRecordInListDto"/>.
/// </summary>
/// <param name="dbContext"></param>
public class GetListOfMaintenanceRecordsQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<MaintenanceRecordInListDto>, List<MaintenanceRecordInListDto>>
{
    public async Task<List<MaintenanceRecordInListDto>> Handle(GetListOfEntityQuery<MaintenanceRecordInListDto> request, CancellationToken cancellationToken)
    {
        var records = await dbContext.MaintenanceRecords
            .Include(r => r.Machine)
                .ThenInclude(m => m.Location)
                    .ThenInclude(l => l.Customer)
            .Select(m => m.ToListDto()).ToListAsync(cancellationToken);

        return records;
    }
}
