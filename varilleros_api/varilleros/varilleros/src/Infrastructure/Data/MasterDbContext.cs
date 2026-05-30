namespace Varilleros.src.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;

public sealed class MasterDbContext(DbContextOptions<MasterDbContext> options) : DbContext(options)
{
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<ModuleCatalog> ModulesCatalog => Set<ModuleCatalog>();
    public DbSet<TenantModule> TenantModules => Set<TenantModule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ── Tenant ──────────────────────────────────────────────────────────
        modelBuilder.Entity<Tenant>(e =>
        {
            e.ToTable("tenants");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            e.Property(x => x.Slug).HasColumnName("slug").HasMaxLength(100).IsRequired();
            e.Property(x => x.DbHost).HasColumnName("db_host").HasMaxLength(255).IsRequired();
            e.Property(x => x.DbPort).HasColumnName("db_port");
            e.Property(x => x.DbName).HasColumnName("db_name").HasMaxLength(100).IsRequired();
            e.Property(x => x.DbUser).HasColumnName("db_user").HasMaxLength(100).IsRequired();
            e.Property(x => x.DbPassword).HasColumnName("db_password").HasMaxLength(500).IsRequired();
            e.Property(x => x.IsActive).HasColumnName("is_active");
            e.Property(x => x.PasswordHash).HasColumnName("password_hash").HasMaxLength(500);
            e.Property(x => x.CreatedAt).HasColumnName("created_at");
            e.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            e.HasIndex(x => x.Slug).IsUnique();
        });

        // ── ModuleCatalog ────────────────────────────────────────────────────
        modelBuilder.Entity<ModuleCatalog>(e =>
        {
            e.ToTable("modules_catalog");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            e.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            e.Property(x => x.Description).HasColumnName("description").HasMaxLength(500).IsRequired();
            e.Property(x => x.IsActive).HasColumnName("is_active");
            e.Property(x => x.CreatedAt).HasColumnName("created_at");
            e.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            e.HasIndex(x => x.Code).IsUnique();
        });

        // ── TenantModule ─────────────────────────────────────────────────────
        modelBuilder.Entity<TenantModule>(e =>
        {
            e.ToTable("tenant_modules");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.TenantId).HasColumnName("tenant_id");
            e.Property(x => x.ModuleId).HasColumnName("module_id");
            e.Property(x => x.IsActive).HasColumnName("is_active");
            e.Property(x => x.GrantedAt).HasColumnName("granted_at");
            e.Property(x => x.ExpiresAt).HasColumnName("expires_at");
            e.Property(x => x.CreatedAt).HasColumnName("created_at");
            e.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        });
    }
}
