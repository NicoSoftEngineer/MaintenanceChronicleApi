using MaintenanceChronicle.Application.Contracts.Locations.Commands;
using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Locations.Commands;

public class CreateNewLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewLocationCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewLocationCommand request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers
            .FirstOrDefaultAsync(c => c.Id == request.LocationDto.CustomerId, cancellationToken);
        if (customer == null) {
            throw new BadRequestException(ErrorType.CustomerNotFound);
        }

        var location = request.LocationDto.ToEntity();
        location.TenantId = Guid.Parse(request.TenantId);
        location.SetCreateBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.Locations.AddAsync(location, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return location.Id;
    }
}
