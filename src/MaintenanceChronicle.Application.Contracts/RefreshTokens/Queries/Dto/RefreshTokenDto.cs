using MaintenanceChronicle.Data.Entities.Account;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.RefreshTokens.Queries.Dto;

public class RefreshTokenDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Token { get; set; }
}

public static class RefreshTokenDtoExtension
{
    public static RefreshTokenDto ToDto(this RefreshToken entity) => new RefreshTokenDto
    {
        Id = entity.Id,
        Token = entity.Token,
        UserId = entity.UserId,
    };
}
