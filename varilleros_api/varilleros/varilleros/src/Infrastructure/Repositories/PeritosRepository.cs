namespace Varilleros.src.Infrastructure.Repositories;

using Dapper;
using Data;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class PeritosRepository(ITenantDbConnectionFactory factory) : IPeritosRepository
{
    public async Task<Perito?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Perito>(
            "SELECT * FROM peritos WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Perito>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<Perito>("SELECT * FROM peritos ORDER BY id");
    }

    public async Task<long> CreateAsync(Perito perito, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            INSERT INTO peritos (nombre, email, created_at, updated_at)
            VALUES (@Nombre, @Email, @CreatedAt, @UpdatedAt)
            """, perito);
        return await conn.ExecuteScalarAsync<long>("SELECT LAST_INSERT_ID()");
    }

    public async Task UpdateAsync(Perito perito, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE peritos SET
                nombre = @Nombre,
                email = @Email,
                updated_at = @UpdatedAt
            WHERE id = @Id
            """, perito);
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM peritos WHERE id = @id", new { id });
    }
}
