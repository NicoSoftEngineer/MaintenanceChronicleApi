using MaintenanceChronicle.Application.Contracts.Locations.Commands;
using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Locations.Commands;
/// <summary>
/// Handler for <see cref="CreateNewLocationCommand"/>
/// </summary>
public class CreateNewLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewLocationCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewLocationCommand request, CancellationToken cancellationToken)
    {
        // Check if customer exists
        var customer = await dbContext.Customers
            .FirstOrDefaultAsync(c => c.Id == request.LocationDto.CustomerId, cancellationToken);
        if (customer == null) {
            throw new BadRequestException(ErrorType.CustomerNotFound);
        }
        // Create new location
        var location = request.LocationDto.ToEntity();
        location.TenantId = Guid.Parse(request.TenantId);
        location.SetCreateBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.Locations.AddAsync(location, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return location.Id;
    }
}
