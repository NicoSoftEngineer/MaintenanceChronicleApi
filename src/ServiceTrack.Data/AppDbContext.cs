using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceTrack.Data.Entities.Account;
using ServiceTrack.Data.Entities.Business;
using ServiceTrack.Utilities.Constants;
using ServiceTrack.Utilities.Helpers;
using System.Reflection.Metadata;

namespace ServiceTrack.Data;

public class AppDbContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly Guid tenantId;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentTenantProvider currentTenantProvider) : base(options)
    {
        tenantId = currentTenantProvider!.TenantId;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

         if (tenantId != Guid.Empty)
         {
            modelBuilder.Entity<Customer>().HasQueryFilter(b => b.TenantId == tenantId);
            modelBuilder.Entity<User>().HasQueryFilter(b => b.TenantId == tenantId);
            modelBuilder.Entity<UserRole>().HasQueryFilter(b => b.TenantId == tenantId);
            modelBuilder.Entity<Tenant>().HasQueryFilter(b => b.Id == tenantId);
         }

        modelBuilder.Entity<UserRole>()
            .HasOne(e => e.Role)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.RoleId)
            .HasPrincipalKey(e => e.Id);
        modelBuilder.Entity<UserRole>()
            .HasOne(e => e.User)
            .WithMany(e => e.Roles)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Role>().HasData(new()
        {
            Id = Guid.Parse("FCD43167-B480-47D9-8A84-7B52C06E1EBB"),
            Name = RoleTypes.GlobalAdmin,
            NormalizedName = RoleTypes.GlobalAdmin.NormalizeToUpper(),
            ConcurrencyStamp = "26AB60F1-DACF-4CF1-996E-DB6E36E36CA5"
        }, new()
        {
            Id = Guid.Parse("3CD34831-A874-47B3-9AB0-5AFD29EDB69E"),
            Name = RoleTypes.Admin,
            NormalizedName = RoleTypes.Admin.NormalizeToUpper(),
            ConcurrencyStamp = "8D8BEBDE-9E70-4AD6-A3E8-34EDD0F7E0CE"
        }, new()
        {
            Id = Guid.Parse("078500E4-4917-4CB0-B6EF-AEEF745BCCA8"),
            Name = RoleTypes.Technician,
            NormalizedName = RoleTypes.Technician.NormalizeToUpper(),
            ConcurrencyStamp = "F5C8757B-9165-4DAA-9076-4CEBD2BD9416"
        });
    }
}
