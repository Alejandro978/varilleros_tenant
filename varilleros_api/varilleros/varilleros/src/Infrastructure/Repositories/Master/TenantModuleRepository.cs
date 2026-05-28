namespace Varilleros.src.Infrastructure.Repositories.Master;

using Dapper;
using Data;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;

public sealed class TenantModuleRepository(IMasterDbConnectionFactory factory) : ITenantModuleRepository
{
    public async Task<TenantModule?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<TenantModule>(
            "SELECT * FROM tenant_modules WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<TenantModule>> GetByTenantIdAsync(int tenantId, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<TenantModule>(
            "SELECT * FROM tenant_modules WHERE tenant_id = @tenantId ORDER BY id", new { tenantId });
    }

    public async Task<TenantModule?> GetByTenantAndModuleAsync(int tenantId, int moduleId, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<TenantModule>(
            "SELECT * FROM tenant_modules WHERE tenant_id = @tenantId AND module_id = @moduleId", new { tenantId, moduleId });
    }

    public async Task<IEnumerable<TenantModule>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<TenantModule>("SELECT * FROM tenant_modules ORDER BY id");
    }

    public async Task<int> CreateAsync(TenantModule tenantModule, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            INSERT INTO tenant_modules (tenant_id, module_id, is_active, granted_at, expires_at, created_at, updated_at)
            VALUES (@TenantId, @ModuleId, @IsActive, @GrantedAt, @ExpiresAt, @CreatedAt, @UpdatedAt)
            """, tenantModule);
        return await conn.ExecuteScalarAsync<int>("SELECT LAST_INSERT_ID()");
    }

    public async Task UpdateAsync(TenantModule tenantModule, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE tenant_modules SET
                is_active = @IsActive,
                expires_at = @ExpiresAt,
                updated_at = @UpdatedAt
            WHERE id = @Id
            """, tenantModule);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM tenant_modules WHERE id = @id", new { id });
    }
}
