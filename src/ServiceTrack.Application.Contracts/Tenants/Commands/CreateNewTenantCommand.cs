using MediatR;
using ServiceTrack.Application.Contracts.Tenants.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Tenants.Commands;

public record CreateNewTenantCommand(NewTenantDto NewTenantDto) : IRequest<Guid>;
