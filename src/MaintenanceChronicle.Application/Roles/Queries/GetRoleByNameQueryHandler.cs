using MaintenanceChronicle.Application.Contracts.Roles.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Roles.Queries;

public class GetRoleByNameQueryHandler(AppDbContext dbContext) : IRequestHandler<GetEntityByNameQuery<RoleDetailDto>,RoleDetailDto>
{
    public async Task<RoleDetailDto> Handle(GetEntityByNameQuery<RoleDetailDto> request, CancellationToken cancellationToken)
    {
        var role = await dbContext.Roles
            .Where(r => r.Name == request.Name)
            .Select(r => new RoleDetailDto
            {
                Id = r.Id,
                Name = r.Name!
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (role == null)
        {
            throw new BadRequestException(ErrorType.RoleNotFound);
        }

        return role;
    }
}
