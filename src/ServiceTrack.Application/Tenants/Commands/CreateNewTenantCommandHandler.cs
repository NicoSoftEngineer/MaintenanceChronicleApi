using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
using ServiceTrack.Application.Contracts.Tenants.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities.Account;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Tenants.Commands;

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
