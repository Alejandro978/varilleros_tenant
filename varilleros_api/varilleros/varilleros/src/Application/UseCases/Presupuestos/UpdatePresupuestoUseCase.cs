namespace Varilleros.src.Application.UseCases.Presupuestos;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class UpdatePresupuestoUseCase(IPresupuestoRepository repo)
{
    public async Task ExecuteAsync(int id, PresupuestoPayload dto, CancellationToken ct = default)
    {
        var presupuesto = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Presupuesto), id);
        presupuesto.Update(dto);
        await repo.UpdateAsync(presupuesto, ct);
    }
}
