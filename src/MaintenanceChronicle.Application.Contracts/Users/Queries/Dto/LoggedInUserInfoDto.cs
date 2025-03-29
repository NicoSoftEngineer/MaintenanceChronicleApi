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
/// <summary>
/// Extension methods for LoggedInUserInfoDto.
/// </summary>
public static class LoggedInUserInfoExtension
{
    /// <summary>
    /// Convert User entity to LoggedInUserInfoDto.
    /// </summary>
    /// <param name="entity">User to convert</param>
    /// <returns>Converted dto</returns>
    public static LoggedInUserInfoDto ToLoggedInUserInfoDto(this User entity) => new LoggedInUserInfoDto
    {
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Email = entity.Email!
    };
}
