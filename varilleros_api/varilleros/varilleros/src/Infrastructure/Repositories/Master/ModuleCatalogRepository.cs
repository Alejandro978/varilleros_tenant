namespace Varilleros.src.Infrastructure.Repositories.Master;

using Dapper;
using Data;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;

public sealed class ModuleCatalogRepository(IMasterDbConnectionFactory factory) : IModuleCatalogRepository
{
    public async Task<ModuleCatalog?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<ModuleCatalog>(
            "SELECT * FROM modules_catalog WHERE id = @id", new { id });
    }

    public async Task<ModuleCatalog?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<ModuleCatalog>(
            "SELECT * FROM modules_catalog WHERE code = @code", new { code });
    }

    public async Task<IEnumerable<ModuleCatalog>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<ModuleCatalog>("SELECT * FROM modules_catalog ORDER BY id");
    }

    public async Task<int> CreateAsync(ModuleCatalog module, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            INSERT INTO modules_catalog (code, name, description, is_active, created_at, updated_at)
            VALUES (@Code, @Name, @Description, @IsActive, @CreatedAt, @UpdatedAt)
            """, module);
        return await conn.ExecuteScalarAsync<int>("SELECT LAST_INSERT_ID()");
    }

    public async Task UpdateAsync(ModuleCatalog module, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE modules_catalog SET
                name = @Name,
                description = @Description,
                is_active = @IsActive,
                updated_at = @UpdatedAt
            WHERE id = @Id
            """, module);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM modules_catalog WHERE id = @id", new { id });
    }
}
