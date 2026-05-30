namespace Varilleros.src.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;
using Varilleros.src.Infrastructure.Data;

public sealed class PresupuestoRepository(TenantDbContext db) : IPresupuestoRepository
{
    public async Task<Presupuesto?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await db.Presupuestos.FindAsync([id], ct);

    public async Task<IEnumerable<Presupuesto>> GetAllAsync(CancellationToken ct = default) =>
        await db.Presupuestos.OrderBy(p => p.Id).ToListAsync(ct);

    public async Task<int> CreateAsync(Presupuesto presupuesto, CancellationToken ct = default)
    {
        db.Presupuestos.Add(presupuesto);
        await db.SaveChangesAsync(ct);
        return presupuesto.Id;
    }

    public async Task UpdateAsync(Presupuesto presupuesto, CancellationToken ct = default) =>
        await db.SaveChangesAsync(ct);

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Presupuestos.FindAsync([id], ct);
        if (entity is not null)
        {
            db.Presupuestos.Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
}
