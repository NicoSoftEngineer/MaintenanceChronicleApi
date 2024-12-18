using MaintenanceChronicle.Application.Contracts.Utils.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.Customers.Commands;

public class DeleteCustomerCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<DeleteEntityByIdCommand<Customer>>
{
    public async Task Handle(DeleteEntityByIdCommand<Customer> request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers.FindAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            throw new BadRequestException(ErrorType.CustomerNotFound);
        }

        customer.SetDeleteBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
