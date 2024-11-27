using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ServiceTrack.Application.Contracts.UserTenant.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities.Account;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.UserTenant.Commands;

public class RegisterUserAndTenantCommandHandler(AppDbContext dbContext, IClock clock, UserManager<User> userManager) : IRequestHandler<RegisterUserAndTenantCommand>
{
    public async Task Handle(RegisterUserAndTenantCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var userTenant = request.UserTenantDto;
            if (await dbContext.Tenants.AnyAsync(x => x.Name == userTenant.TenantName, cancellationToken))
            {
                throw new BadRequestException(ErrorType.NameMustBeUnique);
            }

            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = userTenant.TenantName
            };

            await dbContext.Tenants.AddAsync(tenant, cancellationToken);

            if (await userManager.FindByEmailAsync(userTenant.Email) != null)
            {
                throw new BadRequestException(ErrorType.EmailAlreadyExists);
            }

            var userEntity = new User
            {
                Id = Guid.NewGuid(),
                FirstName = userTenant.FirstName,
                LastName = userTenant.LastName,
                Email = userTenant.Email,
                UserName = userTenant.Email,
                TenantId = tenant.Id,
            };

            userEntity.SetCreateBy(userEntity.Id.ToString(), clock.GetCurrentInstant());
            tenant.SetCreateBy(userEntity.Id.ToString(), clock.GetCurrentInstant());

            var result = await userManager.CreateAsync(userEntity);

            if (!result.Succeeded)
            {
                throw new InternalServerException(result.Errors.Select(e => e.Description).ToList());
            }

            await userManager.AddPasswordAsync(userEntity, userTenant.Password);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw ex;

        }
    }
}
