using MaintenanceChronicle.Application.Contracts.Roles.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data.Entities.Account;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Users.Queries;

public class GetListOfUsersQueryHandler(UserManager<User> userManager) : IRequestHandler<GetListOfEntityQuery<UserListDto>, List<UserListDto>>
{
    public async Task<List<UserListDto>> Handle(GetListOfEntityQuery<UserListDto> request,
        CancellationToken cancellationToken)
    {
        var users = await userManager.Users
            .Include(x => x.Roles)
            .ThenInclude(x => x.Role)
            .Select(x => new UserListDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email!,
                Roles = x.Roles.Select(r => new RoleDetailDto
                {
                    Id = r.Role.Id,
                    Name = r.Role.Name!
                }).ToList()
            })
            .ToListAsync(cancellationToken);
        return users;
    }
}
