using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

public class UpdateUserCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserDetailDto.Id, cancellationToken);
        if (userEntity == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        userEntity.FirstName = request.UserDetailDto.FirstName;
        userEntity.LastName = request.UserDetailDto.LastName;

        userEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
