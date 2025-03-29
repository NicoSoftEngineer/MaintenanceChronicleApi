using MaintenanceChronicle.Data.Entities.Account;

namespace MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;

public class TenantDetailDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}
/// <summary>
/// Extension methods for <see cref="TenantDetailDto"/> entity.
/// </summary>
public static class TenantDetailDtoExtensions
{
    /// <summary>
    /// Converts <see cref="Tenant"/> to <see cref="TenantDetailDto"/>.
    /// </summary>
    /// <param name="tenant"><see cref="Tenant"/> to convert</param>
    /// <returns>Converted <see cref="TenantDetailDto"/></returns>
    public static TenantDetailDto ToDto(this Tenant tenant)
    {
        return new TenantDetailDto
        {
            Id = tenant.Id,
            Name = tenant.Name
        };
    }

    /// <summary>
    /// Maps props from <see cref="TenantDetailDto"/> to <see cref="Tenant"/>.
    /// </summary>
    /// <param name="dto">Source <see cref="TenantDetailDto"/></param>
    /// <param name="target">Destination <see cref="Tenant"/></param>
    public static void MapToEntity(this TenantDetailDto dto, Tenant target)
    {
        target.Id = dto.Id;
        target.Name = dto.Name;
    }
}
