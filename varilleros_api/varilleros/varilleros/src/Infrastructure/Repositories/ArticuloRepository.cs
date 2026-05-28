namespace Varilleros.src.Infrastructure.Repositories;

using Dapper;
using Data;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class ArticuloRepository(ITenantDbConnectionFactory factory) : IArticuloRepository
{
    public async Task<Articulo?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Articulo>(
            "SELECT * FROM articulo WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Articulo>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<Articulo>("SELECT * FROM articulo ORDER BY id");
    }

    public async Task<int> CreateAsync(Articulo articulo, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            INSERT INTO articulo (codigo, descripcion, codigopreciopresupuesto, created_at, updated_at)
            VALUES (@Codigo, @Descripcion, @CodigoPrecioPresupuesto, @CreatedAt, @UpdatedAt)
            """, articulo);
        return await conn.ExecuteScalarAsync<int>("SELECT LAST_INSERT_ID()");
    }

    public async Task UpdateAsync(Articulo articulo, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE articulo SET
                codigo = @Codigo,
                descripcion = @Descripcion,
                codigopreciopresupuesto = @CodigoPrecioPresupuesto,
                updated_at = @UpdatedAt
            WHERE id = @Id
            """, articulo);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM articulo WHERE id = @id", new { id });
    }
}
