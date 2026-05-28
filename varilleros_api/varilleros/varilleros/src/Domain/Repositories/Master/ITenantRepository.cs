namespace Varilleros.src.Domain.Repositories.Master;

using Entities;

public interface ITenantRepository
{
    Task<Tenant?> GetBySlugAsync(string slug, CancellationToken ct = default);
    Task<Tenant?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Tenant>> GetAllAsync(CancellationToken ct = default);
    Task<int> CreateAsync(Tenant tenant, CancellationToken ct = default);
    Task UpdateAsync(Tenant tenant, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
