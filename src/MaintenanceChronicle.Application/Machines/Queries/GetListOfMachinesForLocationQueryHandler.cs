using MaintenanceChronicle.Application.Contracts.Machines.Queries;
using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Machines.Queries;

public class GetMachinesForLocationQueryHandler(AppDbContext dbContext) : IRequestHandler<GetMachinesForLocationQuery, List<MachineInListForLocationDto>>
{
    public async Task<List<MachineInListForLocationDto>> Handle(GetMachinesForLocationQuery request, CancellationToken cancellationToken)
    {
        if (!await dbContext.Locations.AnyAsync(l => l.Id == request.LocationId, cancellationToken: cancellationToken))
        {
            throw new BadRequestException(ErrorType.LocationNotFound);
        }

        var machines = await dbContext.Machines
            .Include(m => m.Location)
            .Where(m => m.LocationId == request.LocationId)
            .Select(m => m.ToMachineInListForLocationDto())
            .ToListAsync(cancellationToken: cancellationToken);

        return machines;
    }
}
