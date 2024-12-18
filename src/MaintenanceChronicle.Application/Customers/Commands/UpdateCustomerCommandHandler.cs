using MaintenanceChronicle.Application.Contracts.Customers.Commands;
using MaintenanceChronicle.Application.Contracts.Customers.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Customers.Commands;

public class UpdateCustomerCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateCustomerCommand, ManageCustomerDetailDto>
{
    public async Task<ManageCustomerDetailDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerEntity = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);
        if (customerEntity == null)
        {
            throw new BadRequestException(ErrorType.CustomerNotFound);
        }
        var patch = request.Patch;

        var customerMapped = customerEntity.ToManageCustomerDetailDto();
        request.Patch.ApplyTo(customerMapped);

        customerEntity.Name = customerMapped.Name;
        customerEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return customerMapped;
    }
}
