using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ServiceTrack.Application.Contracts.Customers.Commands;
using ServiceTrack.Application.Contracts.Locations.Commands;
using ServiceTrack.Application.Contracts.Locations.Commands.Dto;
using ServiceTrack.Application.Validators;
using ServiceTrack.Data;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Locations.Commands;

public class CreateNewLocationCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewLocationCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewLocationCommand request, CancellationToken cancellationToken)
    {
        if (!await dbContext.ValidateUserTenantAccess(request.UserId, request.TenantId))
        {
            throw new BadRequestException(ErrorType.UserNotInTenant);
        }

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
