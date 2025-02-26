using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Users.Commands;

public class ManageRolesForUserCommandHandler(AppDbContext dbContext, IClock clock, UserManager<User> userManager) : IRequestHandler<ManageRolesForUserCommand>
{
    public async Task Handle(ManageRolesForUserCommand request, CancellationToken cancellationToken)
    {
        var userRoles = request.UserRoles;

        var user = await dbContext.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(x => x.Id == userRoles.UserId, cancellationToken: cancellationToken);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var roles = await dbContext.Roles.Where(x => userRoles.RoleIds.Contains(x.Id)).ToListAsync(cancellationToken);

        foreach (var roleToRemove in user.Roles.Where(x => !roles.Contains(x.Role)))
        {
            roleToRemove.SetDeleteBy(request.UserId, clock.GetCurrentInstant());
        }

        var rolesToAdd = roles.Where(x => user.Roles.All(r => r.RoleId != x.Id)).ToList();
        foreach (var roleToAdd in rolesToAdd)
        {
            if (user.Roles.All(x => x.Role.Name != roleToAdd.Name))
            {
                var userRole = new UserRole
                {
                    UserId = userRoles.UserId,
                    RoleId = roleToAdd.Id,
                    TenantId = Guid.Parse(request.TenantId)
                };
                userRole.SetCreateBy(request.UserId, clock.GetCurrentInstant());
                await dbContext.UserRoles.AddAsync(userRole, cancellationToken);
            }
        }
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
