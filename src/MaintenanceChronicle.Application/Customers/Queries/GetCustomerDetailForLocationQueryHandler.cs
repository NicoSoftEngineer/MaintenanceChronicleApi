using MaintenanceChronicle.Application.Contracts.Customers.Queries;
using MaintenanceChronicle.Application.Contracts.Customers.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Customers.Queries;

public class GetCustomerDetailForLocationQueryHandler(AppDbContext dbContext) : IRequestHandler<GetCustomerDetailForLocationQuery, CustomerDetailForLocationDto>
{
    public async Task<CustomerDetailForLocationDto> Handle(GetCustomerDetailForLocationQuery request,
        CancellationToken cancellationToken)
    {
        var locationEntity = await dbContext.Locations
            .Include(l => l.Customer)
            .FirstOrDefaultAsync(l => l.Id == request.LocationId, cancellationToken);
        if (locationEntity == null)
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        var customer = locationEntity.Customer.ToCustomerDetailForLocationDto();
        return customer;
    }
}
