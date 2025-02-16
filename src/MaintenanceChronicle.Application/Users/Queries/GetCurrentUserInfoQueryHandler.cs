using MaintenanceChronicle.Application.Contracts.Users.Queries;
using MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Users.Queries;

public class GetCurrentUserInfoQueryHandler(UserManager<User> userManager) : IRequestHandler<GetCurrentUserInfoQuery, LoggedInUserInfoDto>
{
    public async Task<LoggedInUserInfoDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new BadRequestException(ErrorType.UserNotLoggedIn);
        }

        var userDto = user.ToLoggedInUserInfoDto();
        return userDto;
    }
}
