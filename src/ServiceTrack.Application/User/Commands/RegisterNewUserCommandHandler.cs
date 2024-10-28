using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceTrack.Application.Contracts.User.Commands;
using ServiceTrack.Application.Contracts.User.Commands.Dto;
using ServiceTrack.Data.Entities;

namespace ServiceTrack.Application.User.Commands;

public class RegisterNewUserCommandHandler(UserManager<Data.Entities.User> userManager)
        : IRequestHandler<RegisterNewUserCommand, RegisterNewUserCommandResult>
{
    public async Task<RegisterNewUserCommandResult> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.NewUserDto;

        if(await userManager.FindByEmailAsync(user.Email) != null)
        {
            return RegisterNewUserCommandResult.EmailAlreadyExists;
        }

        var result = await userManager.CreateAsync(new Data.Entities.User
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
