using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaintenanceChronicle.Data.Entities.Account;

namespace MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;
public class LoggedInUserInfoDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}

public static class LoggedInUserInfoExtension
{
    public static LoggedInUserInfoDto ToLoggedInUserInfoDto(this User entity) => new LoggedInUserInfoDto
    {
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Email = entity.Email!
    };
}
