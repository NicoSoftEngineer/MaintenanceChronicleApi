using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Application.Contracts.Users.Queries;
using ServiceTrack.Data;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Queries;

public class GetTenantIdFromUserCommandHandler(AppDbContext dbContext) : IRequestHandler<GetTenantIdFromUserCommand, Guid>
{
    public async Task<Guid> Handle(GetTenantIdFromUserCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }
        return user.TenantId;
    }
}
