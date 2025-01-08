using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries;
using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Utilities.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Queries;

public class GetFilteredMaintenanceRecordsQueryHandler(AppDbContext dbContext): IRequestHandler<GetFilteredMaintenanceRecordsQuery, List<MaintenanceRecordInListDto>>
{
    public async Task<List<MaintenanceRecordInListDto>> Handle(GetFilteredMaintenanceRecordsQuery request,
        CancellationToken cancellationToken)
    {
        var query = dbContext.MaintenanceRecords
            .Include(r => r.Machine)
            .ThenInclude(m => m.Location)
            .AsQueryable();

        if (request.Filter.SearchText != null)
        {
            query = query.Where(r =>
                                        r.Description.Contains(request.Filter.SearchText) ||
                                        r.Machine.Model.Contains(request.Filter.SearchText) ||
                                        r.Machine.SerialNumber.Contains(request.Filter.SearchText) ||
                                        r.Machine.Manufacture.Contains(request.Filter.SearchText) ||
                                        r.Machine.Location.Name.Contains(request.Filter.SearchText) ||
                                        r.Machine.Model.Contains(request.Filter.SearchText));
        }

        if (request.Filter.Machine != Guid.Empty)
        {
            query = query.Where(m => m.MachineId == request.Filter.Machine);
        }

        if (request.Filter.Location != Guid.Empty)
        {
            query = query.Where(r => r.Machine.LocationId == request.Filter.Location);
        }

        if (request.Filter.Type != null)
        {
            query = query.Where(r => r.Type == (RecordType)request.Filter.Type);
        }

        var list = await query.Select(r => r.ToListDto()).ToListAsync(cancellationToken: cancellationToken);

        return list;
    }
}
