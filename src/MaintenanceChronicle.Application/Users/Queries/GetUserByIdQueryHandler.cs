using MaintenanceChronicle.Application.Contracts.Roles.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Users.Queries;

public class GetUserByIdQueryHandler(UserManager<User> userManager) : IRequestHandler<GetEntityByIdQuery<UserDetailDto>, UserDetailDto>
{
    public async Task<UserDetailDto> Handle(GetEntityByIdQuery<UserDetailDto> request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        var userDto = new UserDetailDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Roles = user.Roles.Select(r => new RoleDetailDto
            {
                Id = r.Role.Id,
                Name = r.Role.Name!
            }).ToList()
        };

        return userDto;
    }
}
