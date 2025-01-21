using MaintenanceChronicle.Application.Contracts.Machines.Queries;
using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Machines.Queries;

public class GetFilteredMachinesQueryHandler(AppDbContext dbContext) : IRequestHandler<GetFilteredMachinesQuery, List<MachineInListDto>>
{
    public async Task<List<MachineInListDto>> Handle(GetFilteredMachinesQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Machines
            .Include(x => x.Location)
            .ThenInclude(x => x.Customer)
            .AsQueryable();

        if (request.Filter.SearchText != null)
        {
            query = query.Where(m => m.Model.ToLowerInvariant().Contains(request.Filter.SearchText) ||
                                     m.Manufacture.Contains(request.Filter.SearchText) ||
                                     m.SerialNumber.Contains(request.Filter.SearchText) ||
                                     m.Color.Contains(request.Filter.SearchText) ||
                                     m.Location.Name.Contains(request.Filter.SearchText) ||
                                     m.Location.City.Contains(request.Filter.SearchText) ||
                                     m.Location.Street.Contains(request.Filter.SearchText) ||
                                     m.Location.Country.Contains(request.Filter.SearchText) ||
                                     m.Location.Customer.Name.Contains(request.Filter.SearchText) ||
                                     m.Model.Contains(request.Filter.SearchText));
        }

        if (request.Filter.LocationId != Guid.Empty)
        {
            query = query.Where(x => x.LocationId == request.Filter.LocationId);
        }

        var filteredList = await query.Select(m => m.ToMachineInListDto()).ToListAsync(cancellationToken);
        return filteredList;
    }
}
