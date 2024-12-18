using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Users.Commands;

public class AddRolesToUserCommandHandler(AppDbContext dbContext, IClock clock, UserManager<User> userManager) : IRequestHandler<AddRolesToUserCommand>
{
    public async Task Handle(AddRolesToUserCommand request, CancellationToken cancellationToken)
    {
        var userRoles = request.UserRoles;

        var user = await userManager.FindByIdAsync(userRoles.UserId.ToString());
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var tenant = await dbContext.Tenants.FindAsync(Guid.Parse(request.TenantId), cancellationToken);
        if (tenant == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }

        var roles = await dbContext.Roles.Where(x => userRoles.RoleIds.Contains(x.Id)).ToListAsync(cancellationToken);

        foreach (var rolesToRemove in user.Roles.Where(x => !roles.Contains(x.Role)))
        {
            rolesToRemove.SetDeleteBy(request.UserId, clock.GetCurrentInstant());
        }
        foreach (var rolesToAdd in roles)
        {
            if (user.Roles.All(x => x.Role.Name != rolesToAdd.Name))
            {
                var userRole = new UserRole
                {
                    UserId = userRoles.UserId,
                    RoleId = rolesToAdd.Id,
                    TenantId = tenant.Id
                };
                userRole.SetCreateBy(request.UserId, clock.GetCurrentInstant());
                await dbContext.UserRoles.AddAsync(userRole, cancellationToken);
            }
        }
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
