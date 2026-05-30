namespace Varilleros.src.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;
using Varilleros.src.Infrastructure.Data;

public sealed class PreciosRepository(TenantDbContext db) : IPreciosRepository
{
    public async Task<Precio?> GetByIdAsync(int numeroabolladuras, CancellationToken ct = default) =>
        await db.Precios.FindAsync([numeroabolladuras], ct);

    public async Task<IEnumerable<Precio>> GetAllAsync(CancellationToken ct = default) =>
        await db.Precios.OrderBy(p => p.Numeroabolladuras).ToListAsync(ct);

    public async Task CreateAsync(Precio precio, CancellationToken ct = default)
    {
        db.Precios.Add(precio);
        await db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Precio precio, CancellationToken ct = default) =>
        await db.SaveChangesAsync(ct);

    public async Task DeleteAsync(int numeroabolladuras, CancellationToken ct = default)
    {
        var entity = await db.Precios.FindAsync([numeroabolladuras], ct);
        if (entity is not null)
        {
            db.Precios.Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
}
