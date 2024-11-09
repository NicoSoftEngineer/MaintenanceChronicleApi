using MediatR;
using Microsoft.AspNetCore.Identity;
using NodaTime;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities.Account;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

public class CreateNewUserCommandHandler(UserManager<User> userManager, AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewUserCommand>
{
    public async Task Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        var newUserDto = request.NewUserDto;
        if ((await userManager.FindByEmailAsync(newUserDto.Email)) != null)
        {
            throw new BadRequestException(ErrorType.EmailAlreadyExists);
        }

        var tenant = await dbContext.Tenants.FindAsync(request.TenantId, cancellationToken);
        if (tenant == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }

        var user = new User
        {
            UserName = newUserDto.Email,
            Email = newUserDto.Email,
            FirstName = newUserDto.FirstName,
            LastName = newUserDto.LastName,
            TenantId = tenant.Id,
        };
        user.SetCreateBy(request.UserId, clock.GetCurrentInstant());

        var result = await userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            throw new InternalServerException(result.Errors.Select(e => e.Description).ToList());
        }
    }
}
