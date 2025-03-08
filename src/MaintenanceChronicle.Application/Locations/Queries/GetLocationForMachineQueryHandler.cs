using MaintenanceChronicle.Application.Contracts.Locations.Queries;
using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Locations.Queries;

public class GetLocationForMachineQueryHandler(AppDbContext dbContext) : IRequestHandler<GetLocationForMachineQuery, LocationInListDto>
{
    public async Task<LocationInListDto> Handle(GetLocationForMachineQuery request, CancellationToken cancellationToken)
    {
        var machine = await dbContext.Machines.Include(x => x.Location).ThenInclude(l => l.Customer)
            .FirstOrDefaultAsync(m => m.Id == request.MachineId, cancellationToken);
        if (machine == null)
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        return machine.Location.ToLocationInListDto();
    }
}
