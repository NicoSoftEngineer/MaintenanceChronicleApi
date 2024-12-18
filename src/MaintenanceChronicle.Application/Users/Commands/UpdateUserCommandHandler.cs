using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Users.Commands;

public class UpdateUserCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UpdateUserDetailDto.Id, cancellationToken);
        if (userEntity == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        userEntity.FirstName = request.UpdateUserDetailDto.FirstName;
        userEntity.LastName = request.UpdateUserDetailDto.LastName;

        userEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
