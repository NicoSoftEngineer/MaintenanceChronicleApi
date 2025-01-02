using MaintenanceChronicle.Application.Contracts.RecordTypes.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data.Entities.Business;
using MediatR;

namespace MaintenanceChronicle.Application.RecordTypes.Queries;

public class GetListOfRecordTypesQueryHandler : IRequestHandler<GetListOfEntityQuery<RecordTypeDto>, List<RecordTypeDto>>
{
    public async Task<List<RecordTypeDto>> Handle(GetListOfEntityQuery<RecordTypeDto> request,
        CancellationToken cancellationToken)
    {
        var enumsList = new List<RecordTypeDto>();
        var enums = Enum.GetValuesAsUnderlyingType(typeof(RecordType));
        foreach (RecordType @enum in enums)
        {
            enumsList.Add(@enum.ToDto());
        }

        return enumsList;
    }
}
