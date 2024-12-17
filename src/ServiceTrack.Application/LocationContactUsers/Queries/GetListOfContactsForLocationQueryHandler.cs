using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Application.Contracts.LocationContactUsers.Queries;
using ServiceTrack.Application.Contracts.LocationContactUsers.Queries.Dto;
using ServiceTrack.Data;

namespace ServiceTrack.Application.LocationContactUsers.Queries;

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
