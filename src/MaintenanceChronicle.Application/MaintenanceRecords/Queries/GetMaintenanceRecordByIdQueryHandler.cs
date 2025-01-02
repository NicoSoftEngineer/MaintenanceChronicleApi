using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;

namespace MaintenanceChronicle.Application.MaintenanceRecords.Queries;

public class GetMaintenanceRecordByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetEntityByIdQuery<MaintenanceRecordDetailDto>, MaintenanceRecordDetailDto>
{
    public async Task<MaintenanceRecordDetailDto> Handle(GetEntityByIdQuery<MaintenanceRecordDetailDto> request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.MaintenanceRecords.FindAsync([request.Id], cancellationToken);
        if (entity == null)
        {
            throw new BadRequestException(ErrorType.MaintenanceRecordNotFound);
        }

        return entity.ToDetailDto();
    }
}
