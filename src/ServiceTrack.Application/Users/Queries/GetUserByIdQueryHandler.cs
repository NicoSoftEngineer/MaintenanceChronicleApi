using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Application.Contracts.Roles.Dto;
using ServiceTrack.Application.Contracts.Users.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Data.Entities.Account;

namespace ServiceTrack.Application.Users.Queries;

public class GetUserByIdQueryHandler(UserManager<User> userManager) : IRequestHandler<GetEntityByIdQuery<UserDetailDto>, UserDetailDto>
{
    public async Task<UserDetailDto> Handle(GetEntityByIdQuery<UserDetailDto> request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var userDto = new UserDetailDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles.Select(r => new RoleDetailDto
            {
                Id = r.Role.Id,
                Name = r.Role.Name
            }).ToList()
        };

        return userDto;
    }
}
