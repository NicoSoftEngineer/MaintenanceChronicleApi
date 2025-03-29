using MaintenanceChronicle.Application.Contracts.RecordTypes.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data.Entities.Business;
using MediatR;

namespace MaintenanceChronicle.Application.RecordTypes.Queries;
/// <summary>
/// Handler for <see cref="GetListOfEntityQuery{RecordTypeDto}"/>.
/// </summary>
public class GetListOfRecordTypesQueryHandler : IRequestHandler<GetListOfEntityQuery<RecordTypeDto>, List<RecordTypeDto>>
{
    public Task<List<RecordTypeDto>> Handle(GetListOfEntityQuery<RecordTypeDto> request,
        CancellationToken cancellationToken)
    {
        var enumsList = new List<RecordTypeDto>();
        // get all enum values
        var enums = Enum.GetValuesAsUnderlyingType(typeof(RecordType));
        foreach (RecordType @enum in enums)
        {
            // convert each enum value to dto
            enumsList.Add(@enum.ToDto());
        }

        return Task.FromResult(enumsList);
    }
}
