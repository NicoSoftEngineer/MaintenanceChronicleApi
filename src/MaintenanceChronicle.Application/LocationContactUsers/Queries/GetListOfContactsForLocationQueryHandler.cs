using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries;
using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.LocationContactUsers.Queries;

public class GetListOfContactsForLocationQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfContactsForLocationQuery, List<LocationContactInListDto>>
{
    public async Task<List<LocationContactInListDto>> Handle(GetListOfContactsForLocationQuery request,
        CancellationToken cancellationToken)
    {
        var contacts = await dbContext.LocationContactUsers
            .Include(c => c.User)
            .Include(c => c.Location)
            .Select(c => c.ToLocationContactInListDto())
            .ToListAsync(cancellationToken);

        return contacts;
    }
}
