
using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries;
using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.LocationContactUsers.Queries;
/// <summary>
/// Handler for <see cref="GetListOfLocationsForContactUserQuery"/>
/// </summary>
public class GetListOfLocationsForContactUserQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfLocationsForContactUserQuery, List<LocationInListForContactDto>>
{
    public async Task<List<LocationInListForContactDto>> Handle(GetListOfLocationsForContactUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(u => u.Locations)
            .ThenInclude(l => l.Location)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var locations = user.Locations.Select(u => u.ToListForContactDto()).ToList();

        return locations;
    }
}
