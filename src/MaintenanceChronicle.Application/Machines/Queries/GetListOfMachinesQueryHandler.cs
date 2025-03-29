using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Machines.Queries;
/// <summary>
/// Handler for <see cref="GetListOfEntityQuery{MachineInListDto}"/> to get list of <see cref="MachineInListDto"/>
/// </summary>
/// <param name="dbContext"></param>
public class GetListOfMachinesQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<MachineInListDto>, List<MachineInListDto>>
{
    public async Task<List<MachineInListDto>> Handle(GetListOfEntityQuery<MachineInListDto> request,
        CancellationToken cancellationToken)
    {
        var machines = await dbContext.Machines
            .Include(m => m.Location)
            .ThenInclude(l => l.Customer)
            .Select(m => m.ToMachineInListDto())
            .ToListAsync(cancellationToken: cancellationToken);

        return machines;
    }
}
