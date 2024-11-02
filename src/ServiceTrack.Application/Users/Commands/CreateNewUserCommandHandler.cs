using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Data.Entities;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Users.Commands;

public class CreateNewUserCommandHandler(UserManager<User> userManager) : IRequestHandler<CreateNewUserCommand>
{
    public async Task Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        var newUserDto = request.NewUserDto;
        if (await userManager.FindByEmailAsync(newUserDto.Email) != null)
        {
            throw new BadRequestException(ErrorType.EmailAlreadyExists);
        }

        var user = new User
        {
            UserName = newUserDto.Email,
            Email = newUserDto.Email,
            FirstName = newUserDto.FirstName,
            LastName = newUserDto.LastName
        };

        var result = await userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            throw new InternalServerException(result.Errors.Select(e => e.Description).ToList());
        }
    }
}
