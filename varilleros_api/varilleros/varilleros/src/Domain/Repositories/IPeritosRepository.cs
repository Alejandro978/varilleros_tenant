namespace Varilleros.src.Domain.Repositories;

using Entities;

public interface IPeritosRepository
{
    Task<Perito?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<Perito>> GetAllAsync(CancellationToken ct = default);
    Task<long> CreateAsync(Perito perito, CancellationToken ct = default);
    Task UpdateAsync(Perito perito, CancellationToken ct = default);
    Task DeleteAsync(long id, CancellationToken ct = default);
}
