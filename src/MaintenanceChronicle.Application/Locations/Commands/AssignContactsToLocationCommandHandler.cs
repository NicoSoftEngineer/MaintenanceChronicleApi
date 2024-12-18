using MaintenanceChronicle.Application.Contracts.Locations.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Locations.Commands;

public class ManageContactsInLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<ManageContactsInLocationCommand>
{
    public async Task Handle(ManageContactsInLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await dbContext.Locations
            .Include(l => l.Contacts)
            .FirstOrDefaultAsync(l => l.Id == request.LocationId, cancellationToken);
        if (location == null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        var currentInstant = clock.GetCurrentInstant();

        foreach (var existingContacts in location.Contacts)
        {
            //Remove existing contact if it is not in requests contact list
            if (request.ContactsInLocationDto.ContactIds.All(x => x != existingContacts.UserId))
            {
                existingContacts.SetDeleteBy(request.UserId, currentInstant);
            }
        }

        foreach (var contactId in request.ContactsInLocationDto.ContactIds)
        {
            //Add contact if it is not in existing contact list
            if (location.Contacts.All(x => x.UserId != contactId))
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
                locationContactUser.SetCreateBy(request.UserId, currentInstant);

                await dbContext.AddAsync(locationContactUser, cancellationToken);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
