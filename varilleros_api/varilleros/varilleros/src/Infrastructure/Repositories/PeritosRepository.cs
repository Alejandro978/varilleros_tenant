namespace Varilleros.src.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;
using Varilleros.src.Infrastructure.Data;

public sealed class PeritosRepository(TenantDbContext db) : IPeritosRepository
{
    public async Task<Perito?> GetByIdAsync(long id, CancellationToken ct = default) =>
        await db.Peritos.FindAsync([id], ct);

    public async Task<IEnumerable<Perito>> GetAllAsync(CancellationToken ct = default) =>
        await db.Peritos.OrderBy(p => p.Id).ToListAsync(ct);

    public async Task<long> CreateAsync(Perito perito, CancellationToken ct = default)
    {
        db.Peritos.Add(perito);
        await db.SaveChangesAsync(ct);
        return perito.Id;
    }

    public async Task UpdateAsync(Perito perito, CancellationToken ct = default) =>
        await db.SaveChangesAsync(ct);

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        var entity = await db.Peritos.FindAsync([id], ct);
        if (entity is not null)
        {
            db.Peritos.Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
}
