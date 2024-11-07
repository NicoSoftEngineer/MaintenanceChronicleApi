using MediatR;
using ServiceTrack.Application.Contracts.Tenants.Commands;
using ServiceTrack.Data;
using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Application.Tenants.Commands;

public class UpdateTenantCommandHandler(AppDbContext dbContext) : IRequestHandler<UpdateTenantCommand>
{
    public async Task Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantEntity = await dbContext.Tenants.FindAsync(request.TenantDetail.Id, cancellationToken);
        if (tenantEntity == null)
        {
            throw new BadRequestException(ErrorType.TenantNotFound);
        }

        //map changed properties to entity
        tenantEntity.Name = request.TenantDetail.Name;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}