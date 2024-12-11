using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Application.Contracts.Customers.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Data;

namespace ServiceTrack.Application.Customers.Queries;

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
