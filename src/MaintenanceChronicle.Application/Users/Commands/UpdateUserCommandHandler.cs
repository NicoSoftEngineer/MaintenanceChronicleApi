using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.Users.Commands;
/// <summary>
/// Handler for <see cref="UpdateUserCommand"/>
/// </summary>
public class UpdateUserCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateUserCommand, UpdateUserDetailDto>
{
    public async Task<UpdateUserDetailDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.ModifiedUserId, cancellationToken);
        if (userEntity == null)
        {
            throw new BadRequestException(ErrorType.UserNotFound);
        }

        // map entity to dto
        var userUpdateDto = userEntity.ToUpdateDetail();
        // apply patch to dto
        request.Patch.ApplyTo(userUpdateDto);
        // map changed properties back to entity
        userUpdateDto.MapToEntity(userEntity);

        userEntity.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        // save changes
        await dbContext.SaveChangesAsync(cancellationToken);
        return userUpdateDto;
    }
}
