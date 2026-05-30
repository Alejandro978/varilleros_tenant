namespace Varilleros.src.Infrastructure.Repositories.Master;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;
using Varilleros.src.Infrastructure.Data;

public sealed class TenantRepository(MasterDbContext db) : ITenantRepository
{
    public async Task<Tenant?> GetBySlugAsync(string slug, CancellationToken ct = default) =>
        await db.Tenants.FirstOrDefaultAsync(t => t.Slug == slug, ct);

    public async Task<Tenant?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await db.Tenants.FindAsync([id], ct);

    public async Task<IEnumerable<Tenant>> GetAllAsync(CancellationToken ct = default) =>
        await db.Tenants.OrderBy(t => t.Id).ToListAsync(ct);

    public async Task<int> CreateAsync(Tenant tenant, CancellationToken ct = default)
    {
        db.Tenants.Add(tenant);
        await db.SaveChangesAsync(ct);
        return tenant.Id;
    }

    public async Task UpdateAsync(Tenant tenant, CancellationToken ct = default) =>
        await db.SaveChangesAsync(ct);

    public async Task UpdatePasswordHashAsync(int id, string passwordHash, CancellationToken ct = default)
    {
        var tenant = await db.Tenants.FindAsync([id], ct);
        if (tenant is not null)
        {
            tenant.SetPasswordHash(passwordHash);
            await db.SaveChangesAsync(ct);
        }
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Tenants.FindAsync([id], ct);
        if (entity is not null)
        {
            db.Tenants.Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
}
