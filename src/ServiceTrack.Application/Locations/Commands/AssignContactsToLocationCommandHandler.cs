using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ServiceTrack.Application.Contracts.Locations.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities.Business;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Locations.Commands;

public class AssignContactsToLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<AssignContactsToLocationCommand>
{
    public async Task Handle(AssignContactsToLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await dbContext.Locations
            .Include(l => l.Contacts)
            .FirstOrDefaultAsync(l => l.Id == request.ContactsToLocationDto.LocationId, cancellationToken);
        if (location == null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        foreach (var contactId in request.ContactsToLocationDto.ContactIds)
        {
            var user = await dbContext.Users.FindAsync(new object[] { contactId }, cancellationToken);
            if (user == null)
            {
                throw new BadRequestException(ErrorType.UserNotFound);
            }

            var locationContactUser = new LocationContactUser
            {
                UserId = user.Id,
                LocationId = location.Id,
                TenantId = Guid.Parse(request.TenantId)
            };
            locationContactUser.SetCreateBy(request.UserId, clock.GetCurrentInstant());

            await dbContext.AddAsync(locationContactUser, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
