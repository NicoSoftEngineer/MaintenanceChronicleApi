using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ServiceTrack.Application.Contracts.Customers.Commands;
using ServiceTrack.Application.Contracts.Customers.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Customers.Commands;

public class UpdateCustomerCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateCustomerCommand, CustomerDetailDto>
{
    public async Task<CustomerDetailDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerEntity = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);
        if (customerEntity == null)
        {
            throw new BadRequestException(ErrorType.CustomerNotFound);
        }
        var patch = request.Patch;

        var customerMapped = customerEntity.ToCustomerDetailDto();
        request.Patch.ApplyTo(customerMapped);

        customerEntity.Name = customerMapped.Name;
        customerEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return customerMapped;
    }
}
