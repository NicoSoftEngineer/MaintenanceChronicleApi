using MaintenanceChronicle.Application.Contracts.Customers.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Customers.Queries;

public class GetListOfCustomersQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<CustomerInListDto>,List<CustomerInListDto>>
{
    public async Task<List<CustomerInListDto>> Handle(GetListOfEntityQuery<CustomerInListDto> request,
        CancellationToken cancellationToken)
    {
        var customers = await dbContext.Customers
            .Select(c => new CustomerInListDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync(cancellationToken);

        return customers;
    }
}
