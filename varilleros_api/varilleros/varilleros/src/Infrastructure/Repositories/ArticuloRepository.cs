namespace Varilleros.src.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;
using Varilleros.src.Infrastructure.Data;

public sealed class ArticuloRepository(TenantDbContext db) : IArticuloRepository
{
    public async Task<Articulo?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await db.Articulos.FindAsync([id], ct);

    public async Task<IEnumerable<Articulo>> GetAllAsync(CancellationToken ct = default) =>
        await db.Articulos.OrderBy(a => a.Id).ToListAsync(ct);

    public async Task<int> CreateAsync(Articulo articulo, CancellationToken ct = default)
    {
        db.Articulos.Add(articulo);
        await db.SaveChangesAsync(ct);
        return articulo.Id;
    }

    public async Task UpdateAsync(Articulo articulo, CancellationToken ct = default) =>
        await db.SaveChangesAsync(ct);

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Articulos.FindAsync([id], ct);
        if (entity is not null)
        {
            db.Articulos.Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
}
