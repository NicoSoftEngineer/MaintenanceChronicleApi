using MaintenanceChronicle.Application.Contracts.Customers.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;

namespace MaintenanceChronicle.Application.Customers.Queries;
/// <summary>
/// Handler for <see cref="GetEntityByIdQuery{CustomerDetailDto}"/> to get a customer by ID.
/// </summary>
public class GetCustomerByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetEntityByIdQuery<CustomerDetailDto>, CustomerDetailDto>
{
    public async Task<CustomerDetailDto> Handle(GetEntityByIdQuery<CustomerDetailDto> request,
        CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers.FindAsync([request.Id ], cancellationToken);

        if (customer == null)
        {
            throw new BadRequestException(ErrorType.CustomerNotFound);
        }

        var customerDetailDto = customer.ToCustomerDetailDto();

        return customerDetailDto;
    }
}
