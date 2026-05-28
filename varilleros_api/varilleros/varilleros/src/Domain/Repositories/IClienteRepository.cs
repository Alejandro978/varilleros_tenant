namespace Varilleros.src.Domain.Repositories;

using Entities;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Cliente>> GetAllAsync(CancellationToken ct = default);
    Task<int> CreateAsync(Cliente cliente, CancellationToken ct = default);
    Task UpdateAsync(Cliente cliente, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
