using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;
/// <summary>
/// Checks if user can log in using provided credentials.
/// </summary>
/// <param name="Login">Dto with credentials</param>
/// <returns>SignInResult</returns>
public record CheckUserLogInCommand(LoginDto Login) : IRequest<SignInResult>;
