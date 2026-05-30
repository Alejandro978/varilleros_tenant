namespace Varilleros.src.Infrastructure.Repositories.Master;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;
using Varilleros.src.Infrastructure.Data;

public sealed class TenantModuleRepository(MasterDbContext db) : ITenantModuleRepository
{
    public async Task<TenantModule?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await db.TenantModules.FindAsync([id], ct);

    public async Task<IEnumerable<TenantModule>> GetByTenantIdAsync(int tenantId, CancellationToken ct = default) =>
        await db.TenantModules.Where(tm => tm.TenantId == tenantId).OrderBy(tm => tm.Id).ToListAsync(ct);

    public async Task<TenantModule?> GetByTenantAndModuleAsync(int tenantId, int moduleId, CancellationToken ct = default) =>
        await db.TenantModules.FirstOrDefaultAsync(tm => tm.TenantId == tenantId && tm.ModuleId == moduleId, ct);

    public async Task<IEnumerable<TenantModule>> GetAllAsync(CancellationToken ct = default) =>
        await db.TenantModules.OrderBy(tm => tm.Id).ToListAsync(ct);

    public async Task<int> CreateAsync(TenantModule tenantModule, CancellationToken ct = default)
    {
        db.TenantModules.Add(tenantModule);
        await db.SaveChangesAsync(ct);
        return tenantModule.Id;
    }

    public async Task UpdateAsync(TenantModule tenantModule, CancellationToken ct = default) =>
        await db.SaveChangesAsync(ct);

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.TenantModules.FindAsync([id], ct);
        if (entity is not null)
        {
            db.TenantModules.Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
}
