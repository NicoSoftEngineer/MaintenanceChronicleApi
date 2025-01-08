using MaintenanceChronicle.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Api.Utils;

public static class DatabaseMigrationExtension
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        return app;
    }
}
