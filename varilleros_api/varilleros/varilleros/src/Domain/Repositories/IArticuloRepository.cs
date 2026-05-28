namespace Varilleros.src.Domain.Repositories;

using Entities;

public interface IArticuloRepository
{
    Task<Articulo?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Articulo>> GetAllAsync(CancellationToken ct = default);
    Task<int> CreateAsync(Articulo articulo, CancellationToken ct = default);
    Task UpdateAsync(Articulo articulo, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
