using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record CheckUserLogInCommand(LoginDto Login) : IRequest<SignInResult>;
