using MediatR;
using NodaTime;
using ServiceTrack.Application.Contracts.Utils.Commands;
using ServiceTrack.Application.Validators;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities.Business;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Locations.Commands;

public class DeleteLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<DeleteEntityByIdCommand<Location>>
{
    public async Task Handle(DeleteEntityByIdCommand<Location> request, CancellationToken cancellationToken)
    {
        var location = await dbContext.Locations.FindAsync(request.Id,cancellationToken);
        if (location == null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        location.SetDeleteBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
