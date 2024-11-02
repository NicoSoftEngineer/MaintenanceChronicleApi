using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data.Entities;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

public class RegisterNewUserCommandHandler(UserManager<User> userManager)
        : IRequestHandler<RegisterNewUserCommand>
{
    public async Task Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.NewUserDto;

        if (await userManager.FindByEmailAsync(user.Email) != null)
        {
            throw new BadRequestException(ErrorType.EmailAlreadyExists);
        }

        var result = await userManager.CreateAsync(new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.Email
        });

        if (!result.Succeeded)
        {
            throw new InternalServerException(result.Errors.Select(e => e.Description).ToList());
        }
    }
}
