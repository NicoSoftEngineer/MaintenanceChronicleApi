using MediatR;
using NodaTime;
using ServiceTrack.Application.Contracts.Utils.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities.Business;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Customers.Commands;

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
