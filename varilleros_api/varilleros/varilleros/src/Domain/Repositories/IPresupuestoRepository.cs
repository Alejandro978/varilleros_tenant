namespace Varilleros.src.Domain.Repositories;

using Entities;

public interface IPresupuestoRepository
{
    Task<Presupuesto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Presupuesto>> GetAllAsync(CancellationToken ct = default);
    Task<int> CreateAsync(Presupuesto presupuesto, CancellationToken ct = default);
    Task UpdateAsync(Presupuesto presupuesto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
