using MaintenanceChronicle.Application.Contracts.Customers.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Data.Interfaces;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Customers.Commands;

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
