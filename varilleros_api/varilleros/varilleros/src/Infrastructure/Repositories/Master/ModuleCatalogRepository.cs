namespace Varilleros.src.Infrastructure.Repositories.Master;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;
using Varilleros.src.Infrastructure.Data;

public sealed class ModuleCatalogRepository(MasterDbContext db) : IModuleCatalogRepository
{
    public async Task<ModuleCatalog?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await db.ModulesCatalog.FindAsync([id], ct);

    public async Task<ModuleCatalog?> GetByCodeAsync(string code, CancellationToken ct = default) =>
        await db.ModulesCatalog.FirstOrDefaultAsync(m => m.Code == code, ct);

    public async Task<IEnumerable<ModuleCatalog>> GetAllAsync(CancellationToken ct = default) =>
        await db.ModulesCatalog.OrderBy(m => m.Id).ToListAsync(ct);

    public async Task<int> CreateAsync(ModuleCatalog module, CancellationToken ct = default)
    {
        db.ModulesCatalog.Add(module);
        await db.SaveChangesAsync(ct);
        return module.Id;
    }

    public async Task UpdateAsync(ModuleCatalog module, CancellationToken ct = default) =>
        await db.SaveChangesAsync(ct);

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.ModulesCatalog.FindAsync([id], ct);
        if (entity is not null)
        {
            db.ModulesCatalog.Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
}
