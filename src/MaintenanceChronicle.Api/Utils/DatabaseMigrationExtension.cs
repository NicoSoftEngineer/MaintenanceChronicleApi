using MaintenanceChronicle.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Api.Utils;

public static class DatabaseMigrationExtension
{
    /// <summary>
    /// Applies migrations to DB.
    /// Asynchronous
    /// </summary>
    /// <param name="app">WebApplication on which you want the Db to be updated</param>
    /// <returns>WebApplication</returns>
    public static async Task<WebApplication> ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
        return app;
    }
}
