namespace Varilleros.src.Infrastructure.Repositories.Master;

using Dapper;
using Data;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;

public sealed class TenantRepository(IMasterDbConnectionFactory factory) : ITenantRepository
{
    public async Task<Tenant?> GetBySlugAsync(string slug, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Tenant>(
            "SELECT * FROM tenants WHERE slug = @slug", new { slug });
    }

    public async Task<Tenant?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Tenant>(
            "SELECT * FROM tenants WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Tenant>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<Tenant>("SELECT * FROM tenants ORDER BY id");
    }

    public async Task<int> CreateAsync(Tenant tenant, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            INSERT INTO tenants (name, slug, db_host, db_port, db_name, db_user, db_password, password_hash, is_active, created_at, updated_at)
            VALUES (@Name, @Slug, @DbHost, @DbPort, @DbName, @DbUser, @DbPassword, @PasswordHash, @IsActive, @CreatedAt, @UpdatedAt)
            """, new
        {
            tenant.Name,
            tenant.Slug,
            tenant.DbHost,
            tenant.DbPort,
            tenant.DbName,
            tenant.DbUser,
            tenant.DbPassword,
            tenant.PasswordHash,
            tenant.IsActive,
            tenant.CreatedAt,
            tenant.UpdatedAt,
        });
        return await conn.ExecuteScalarAsync<int>("SELECT LAST_INSERT_ID()");
    }

    public async Task UpdateAsync(Tenant tenant, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE tenants SET
                name = @Name,
                db_host = @DbHost,
                db_port = @DbPort,
                db_name = @DbName,
                db_user = @DbUser,
                db_password = @DbPassword,
                is_active = @IsActive,
                updated_at = @UpdatedAt
            WHERE id = @Id
            """, new
        {
            tenant.Id,
            tenant.Name,
            tenant.DbHost,
            tenant.DbPort,
            tenant.DbName,
            tenant.DbUser,
            tenant.DbPassword,
            tenant.IsActive,
            tenant.UpdatedAt,
        });
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM tenants WHERE id = @id", new { id });
    }

    public async Task UpdatePasswordHashAsync(int id, string passwordHash, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync(
            "UPDATE tenants SET password_hash = @passwordHash, updated_at = UTC_TIMESTAMP() WHERE id = @id",
            new { id, passwordHash });
    }
}
