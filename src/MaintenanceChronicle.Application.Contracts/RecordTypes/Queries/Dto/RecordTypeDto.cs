using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Utilities.Enum;

namespace MaintenanceChronicle.Application.Contracts.RecordTypes.Queries.Dto;

public class RecordTypeDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
/// <summary>
/// Extension methods for <see cref="RecordType"/> enum.
/// </summary>
public static class RecordTypeExtension
{
    /// <summary>
    /// Converts <see cref="RecordType"/> to <see cref="RecordTypeDto"/>.
    /// </summary>
    /// <param name="entity">Enum entity</param>
    /// <returns>Mapped entity to dto</returns>
    public static RecordTypeDto ToDto(this RecordType entity) =>
        new RecordTypeDto { Id = (int)entity, Name = entity.GetTypeName() };
}
