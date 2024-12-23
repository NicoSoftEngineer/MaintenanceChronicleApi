using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;

namespace MaintenanceChronicle.Application.Machines.Queries;

public class GetMachineByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetEntityByIdQuery<MachineDetailDto>, MachineDetailDto>
{
    public async Task<MachineDetailDto> Handle(GetEntityByIdQuery<MachineDetailDto> request,
        CancellationToken cancellationToken)
    {
        var machine = await dbContext.Machines.FindAsync(new object[] { request.Id }, cancellationToken);
        if (machine is null)
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        return machine.ToMachineDetailDto();
    }
}
