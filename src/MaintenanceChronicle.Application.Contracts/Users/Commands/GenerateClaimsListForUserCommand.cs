using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

/// <summary>
/// Generates claim list for user,
/// </summary>
/// <param name="UserEmail">Email</param>
/// <returns>Returns list with userId, email, userName</returns>
public record GenerateClaimsListForUserCommand(string UserEmail) : IRequest<List<Claim>>;
