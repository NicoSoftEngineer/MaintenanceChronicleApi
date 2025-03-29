using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Locations.Queries;
/// <summary>
/// Handler for <see cref="GetListOfAllPossibleContactsQuery"/>.
/// </summary>
public class GetListOfAllPossibleContactsQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<LocationContactInListDto>, List<LocationContactInListDto>>
{
    public async Task<List<LocationContactInListDto>> Handle(GetListOfEntityQuery<LocationContactInListDto> request,
        CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.Select(u => u.ToLocationContactInListDto()).ToListAsync(cancellationToken);
        return users;
    }
}
