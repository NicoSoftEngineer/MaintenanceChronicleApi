using MediatR;
using NodaTime;
using ServiceTrack.Application.Contracts.Customers.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities.Business;
using ServiceTrack.Data.Interfaces;

namespace ServiceTrack.Application.Customers.Commands;

public class CreateNewCustomerCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerDto = request.NewCustomer;
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = customerDto.Name,
            TenantId = Guid.Parse(request.TenantId)
        };
        customer.SetCreateBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.Customers.AddAsync(customer, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
