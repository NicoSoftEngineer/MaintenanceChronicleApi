
using MaintenanceChronicle.Application.Contracts.Roles.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Roles.Queries;
/// <summary>
/// Handler for <see cref="GetListOfEntityQuery{RoleDetailDto}"/> to get list of roles.
/// </summary>
public class GetListOfRolesQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<RoleDetailDto>, List<RoleDetailDto>>
{
    public async Task<List<RoleDetailDto>> Handle(GetListOfEntityQuery<RoleDetailDto> request,
        CancellationToken cancellationToken)
    {
        var roles = await dbContext.Roles
            .Select(r => new RoleDetailDto
            {
                Id = r.Id,
                Name = r.Name!
            })
            .ToListAsync(cancellationToken);

        roles.Remove(roles.First(r => r.Name == RoleTypes.GlobalAdmin));

        return roles;
    }
}
