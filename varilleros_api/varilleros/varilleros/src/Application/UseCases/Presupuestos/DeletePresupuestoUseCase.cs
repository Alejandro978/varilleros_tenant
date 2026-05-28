namespace Varilleros.src.Application.UseCases.Presupuestos;

using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class DeletePresupuestoUseCase(IPresupuestoRepository repo)
{
    public async Task ExecuteAsync(int id, CancellationToken ct = default)
    {
        _ = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Presupuesto), id);
        await repo.DeleteAsync(id, ct);
    }
}
