using MediatR;
using ServiceTrack.Application.Contracts.Customers.Commands.Dto;
using ServiceTrack.Application.Contracts.Customers.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Data;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Customers.Queries;

public class GetCustomerByIdCommandHandler(AppDbContext dbContext) : IRequestHandler<GetEntityByIdQuery<CustomerDetailDto>, CustomerDetailDto>
{
    public async Task<CustomerDetailDto> Handle(GetEntityByIdQuery<CustomerDetailDto> request,
        CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers.FindAsync(request.Id, cancellationToken);

        if (customer == null)
        {
            throw new BadRequestException(ErrorType.CustomerNotFound);
        }

        var customerDetailDto = customer.ToCustomerDetailDto();

        return customerDetailDto;
    }
}
