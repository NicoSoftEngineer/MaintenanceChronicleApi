using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Data.Entities;

namespace ServiceTrack.Application.Users.Commands;

public class RegisterNewUserCommandHandler(UserManager<User> userManager)
        : IRequestHandler<RegisterNewUserCommand, RegisterNewUserCommandResult>
{
    public async Task<RegisterNewUserCommandResult> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.NewUserDto;

        if (await userManager.FindByEmailAsync(user.Email) != null)
        {
            return RegisterNewUserCommandResult.EmailAlreadyExists;
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
            throw new Exception("Failed to create user");
        }

        return RegisterNewUserCommandResult.Success;
    }
}
