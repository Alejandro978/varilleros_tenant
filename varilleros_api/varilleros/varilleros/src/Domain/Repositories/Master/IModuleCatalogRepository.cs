namespace Varilleros.src.Domain.Repositories.Master;

using Entities;

public interface IModuleCatalogRepository
{
    Task<ModuleCatalog?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ModuleCatalog?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<IEnumerable<ModuleCatalog>> GetAllAsync(CancellationToken ct = default);
    Task<int> CreateAsync(ModuleCatalog module, CancellationToken ct = default);
    Task UpdateAsync(ModuleCatalog module, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
