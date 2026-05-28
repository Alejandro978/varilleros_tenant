namespace Varilleros.src.Domain.Repositories.Master;

using Entities;

public interface ITenantModuleRepository
{
    Task<TenantModule?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<TenantModule>> GetByTenantIdAsync(int tenantId, CancellationToken ct = default);
    Task<TenantModule?> GetByTenantAndModuleAsync(int tenantId, int moduleId, CancellationToken ct = default);
    Task<IEnumerable<TenantModule>> GetAllAsync(CancellationToken ct = default);
    Task<int> CreateAsync(TenantModule tenantModule, CancellationToken ct = default);
    Task UpdateAsync(TenantModule tenantModule, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
