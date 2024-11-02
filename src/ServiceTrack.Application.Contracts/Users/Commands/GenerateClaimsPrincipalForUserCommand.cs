using System.Security.Claims;
using MediatR;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Users.Commands;

public record GenerateClaimsPrincipalForUserCommand(LoginDto UserLogin) : IRequest<ClaimsPrincipal>;
