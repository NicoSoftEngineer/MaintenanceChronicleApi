using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Tenants.Commands;

public class CreateNewTenantCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewTenantCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewTenantCommand request, CancellationToken cancellationToken)
    {
        var newTenantDto = request.NewTenantDto;
        if (await dbContext.Tenants.AnyAsync(x => x.Name == newTenantDto.Name, cancellationToken))
        {
            throw new BadRequestException(ErrorType.NameMustBeUnique);
        }

        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = newTenantDto.Name
        };
        tenant.SetCreateBySystem(clock.GetCurrentInstant());

        await dbContext.Tenants.AddAsync(tenant, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return tenant.Id;
    }
}
