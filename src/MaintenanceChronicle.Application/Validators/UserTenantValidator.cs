using MaintenanceChronicle.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.Validators;

public static class UserTenantValidator
{
    /// <summary>
    /// Validates if user and tenant exist, and if user has access to tenant
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="userId"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static async Task<bool> ValidateUserTenantAccess(this AppDbContext dbContext, string userId, string tenantId)
    {
        var user = await dbContext.Users.Include(x => x.Tenant).FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
        var tenant = await dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == Guid.Parse(tenantId));

        if (tenant != null && user != null)
        {
            if (user.TenantId == tenant.Id)
            {
                return true;
            }
        }

        return false;
    }
}
