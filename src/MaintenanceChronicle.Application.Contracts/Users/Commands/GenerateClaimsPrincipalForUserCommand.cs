using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record GenerateClaimsPrincipalForUserCommand(LoginDto UserLogin) : IRequest<ClaimsPrincipal>;
