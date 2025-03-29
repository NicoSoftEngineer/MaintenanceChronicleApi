using System.Runtime.InteropServices;
using MaintenanceChronicle.Application.Contracts.Locations.Queries;
using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Locations.Queries;
/// <summary>
/// Handler for <see cref="GetLocationsForCustomerQuery"/>.
/// </summary>
public class GetLocationsForCustomerQueryHandler(AppDbContext dbContext) : IRequestHandler<GetLocationsForCustomerQuery,List<LocationInListDto>>
{
    public async Task<List<LocationInListDto>> Handle(GetLocationsForCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var locations = await dbContext.Locations
            .Include(l => l.Customer)
            .Where(l => l.CustomerId == request.CustomerId)
            .Select(x => x.ToLocationInListDto())
            .ToListAsync(cancellationToken);

        return locations;
    }
}
