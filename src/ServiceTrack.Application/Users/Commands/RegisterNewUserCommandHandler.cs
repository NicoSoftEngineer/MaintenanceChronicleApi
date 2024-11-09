using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Entities.Account;
using ServiceTrack.Data.Interfaces;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

public class RegisterNewUserCommandHandler(UserManager<User> userManager, AppDbContext dbContext, IClock clock)
        : IRequestHandler<RegisterNewUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.NewUserDto;

        if (await userManager.FindByEmailAsync(user.Email) != null)
        {
            throw new BadRequestException(ErrorType.EmailAlreadyExists);
        }

        var tenant = await dbContext.Tenants.FirstOrDefaultAsync(x => x.Id == user.TenantId, cancellationToken);
        if (tenant == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }

        var result = await userManager.CreateAsync(new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.Email,
            TenantId = tenant.Id,
        });

        if (!result.Succeeded)
        {
            throw new InternalServerException(result.Errors.Select(e => e.Description).ToList());
        }

        var newUser = await dbContext.Users.FirstAsync(x => x.Email == user.Email); 
        newUser!.SetCreateBy(newUser!.Id.ToString(), clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}
