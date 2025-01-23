using MaintenanceChronicle.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Api.Utils;

public static class DatabaseMigrationExtension
{
    public static async Task<WebApplication> ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
        return app;
    }
}
