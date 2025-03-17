using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

/// <summary>
/// Generates claim list for user,
/// </summary>
/// <param name="UserLogin">Username/Email and password model</param>
/// <returns>Returns list with userId, email, userName</returns>
public record GenerateClaimsListForUserCommand(LoginDto UserLogin) : IRequest<List<Claim>>;
