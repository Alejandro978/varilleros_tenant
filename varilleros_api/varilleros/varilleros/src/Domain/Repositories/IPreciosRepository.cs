namespace Varilleros.src.Domain.Repositories;

using Entities;

public interface IPreciosRepository
{
    Task<Precio?> GetByIdAsync(int numeroabolladuras, CancellationToken ct = default);
    Task<IEnumerable<Precio>> GetAllAsync(CancellationToken ct = default);
    Task CreateAsync(Precio precio, CancellationToken ct = default);
    Task UpdateAsync(Precio precio, CancellationToken ct = default);
    Task DeleteAsync(int numeroabolladuras, CancellationToken ct = default);
}
