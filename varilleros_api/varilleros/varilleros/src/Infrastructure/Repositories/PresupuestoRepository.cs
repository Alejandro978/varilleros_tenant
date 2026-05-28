namespace Varilleros.src.Infrastructure.Repositories;

using Dapper;
using Data;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class PresupuestoRepository(ITenantDbConnectionFactory factory) : IPresupuestoRepository
{
    public async Task<Presupuesto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Presupuesto>(
            "SELECT * FROM presupuesto WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Presupuesto>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<Presupuesto>("SELECT * FROM presupuesto ORDER BY id");
    }

    public async Task<int> CreateAsync(Presupuesto presupuesto, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            INSERT INTO presupuesto (cliente_id, perito_id, fecha_presupuesto, descripcion, total_presupuesto, estado, created_at, updated_at)
            VALUES (@ClienteId, @PeritoId, @FechaPresupuesto, @Descripcion, @TotalPresupuesto, @Estado, @CreatedAt, @UpdatedAt)
            """, presupuesto);
        return await conn.ExecuteScalarAsync<int>("SELECT LAST_INSERT_ID()");
    }

    public async Task UpdateAsync(Presupuesto presupuesto, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE presupuesto SET
                cliente_id = @ClienteId,
                perito_id = @PeritoId,
                descripcion = @Descripcion,
                total_presupuesto = @TotalPresupuesto,
                estado = @Estado,
                updated_at = @UpdatedAt
            WHERE id = @Id
            """, presupuesto);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM presupuesto WHERE id = @id", new { id });
    }
}
