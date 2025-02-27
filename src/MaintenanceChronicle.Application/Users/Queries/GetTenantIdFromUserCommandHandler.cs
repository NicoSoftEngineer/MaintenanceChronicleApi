using MaintenanceChronicle.Application.Contracts.Users.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Users.Queries;

public class GetTenantIdFromUserCommandHandler(AppDbContext dbContext) : IRequestHandler<GetTenantIdFromUserQuery, Guid>
{
    public async Task<Guid> Handle(GetTenantIdFromUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }
        return user.TenantId;
    }
}
