using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Utilities.Enum;

namespace MaintenanceChronicle.Application.Contracts.RecordTypes.Queries.Dto;

public class RecordTypeDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public static class RecordTypeExtension
{
    public static RecordTypeDto ToDto(this RecordType entity) =>
        new RecordTypeDto { Id = (int)entity, Name = entity.GetTypeName() };
}
