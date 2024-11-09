using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ServiceTrack.Application.Contracts.Tenants.Commands;
using ServiceTrack.Data;
using ServiceTrack.Data.Interfaces;

namespace ServiceTrack.Application.Tenants.Commands;

public class AssignCreationInfoToTenantCommandHandler(AppDbContext dbContext, IClock clock)
    : IRequestHandler<AssignCreationInfoToTenantCommand>
{
    public async Task Handle(AssignCreationInfoToTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await dbContext.Tenants.FirstAsync(x => x.Id == request.tenantsCreatorUserIdDto.TenantId, cancellationToken);
        tenant.SetCreateBy(request.tenantsCreatorUserIdDto.UserId, clock.GetCurrentInstant());
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
