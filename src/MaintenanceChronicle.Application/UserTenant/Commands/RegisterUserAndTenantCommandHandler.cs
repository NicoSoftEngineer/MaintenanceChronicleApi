using MaintenanceChronicle.Application.Contracts.UserTenant.Commands;
using MaintenanceChronicle.Application.Contracts.UserTenant.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.UserTenant.Commands;
/// <summary>
/// Command handler for <see cref="RegisterUserAndTenantCommand"/>
/// </summary>
public class RegisterUserAndTenantCommandHandler(AppDbContext dbContext, IClock clock, UserManager<User> userManager) : IRequestHandler<RegisterUserAndTenantCommand, UserTenantIdsDto>
{
    public async Task<UserTenantIdsDto> Handle(RegisterUserAndTenantCommand request, CancellationToken cancellationToken)
    {
        // Begin transaction
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            // Check if tenant name is unique
            var userTenant = request.RegisterUserTenantDto;
            if (await dbContext.Tenants.AnyAsync(x => x.Name == userTenant.TenantName, cancellationToken))
            {
                throw new BadRequestException(ErrorType.NameMustBeUnique, "tenantName");
            }

            // Create tenant entity
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = userTenant.TenantName
            };

            await dbContext.Tenants.AddAsync(tenant, cancellationToken);

            // Check if email is unique
            if (await userManager.FindByEmailAsync(userTenant.Email) != null)
            {
                throw new BadRequestException(ErrorType.EmailAlreadyExists, "email");
            }

            // Create user entity
            var userEntity = new User
            { 
                Id = Guid.NewGuid(),
                FirstName = userTenant.FirstName,
                LastName = userTenant.LastName,
                Email = userTenant.Email,
                PhoneNumber = userTenant.PhoneNumber,
                UserName = userTenant.Email,
                TenantId = tenant.Id,
            };

            // Set create by for user and tenant
            userEntity.SetCreateBy(userEntity.Id.ToString(), clock.GetCurrentInstant());
            tenant.SetCreateBy(userEntity.Id.ToString(), clock.GetCurrentInstant());

            // Create user
            var result = await userManager.CreateAsync(userEntity);

            if (!result.Succeeded)
            {
                throw new InternalServerException(result.Errors.Select(e => e.Description).ToList());
            }

            // Add password to user
            await userManager.AddPasswordAsync(userEntity, userTenant.Password);

            // Commit transaction to db
            await transaction.CommitAsync(cancellationToken);

            // Return user and tenant ids
            return new UserTenantIdsDto
            {
                UserId = userEntity.Id,
                TenantId = tenant.Id
            };
        }
        catch
        {
            // Rollback transaction if anything goes wrong
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
