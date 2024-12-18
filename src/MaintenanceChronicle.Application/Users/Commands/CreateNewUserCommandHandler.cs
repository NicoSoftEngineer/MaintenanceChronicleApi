using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace MaintenanceChronicle.Application.Users.Commands;

public class CreateNewUserCommandHandler(UserManager<User> userManager, AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        var newUserDto = request.NewUserDto;
        if ((await userManager.FindByEmailAsync(newUserDto.Email)) != null)
        {
            throw new BadRequestException(ErrorType.EmailAlreadyExists);
        }

        var tenant = await dbContext.Tenants.FindAsync(Guid.Parse(request.TenantId), cancellationToken);
        if (tenant == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
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

        return user.Id;
    }
}
