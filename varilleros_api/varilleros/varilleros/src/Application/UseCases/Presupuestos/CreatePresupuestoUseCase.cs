namespace Varilleros.src.Application.UseCases.Presupuestos;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class CreatePresupuestoUseCase(IPresupuestoRepository repo)
{
    public async Task<int> ExecuteAsync(CreatePresupuestoDto dto, CancellationToken ct = default)
    {
        var presupuesto = Presupuesto.Create(
            dto.ClienteId, dto.PeritoId, dto.Descripcion, dto.TotalPresupuesto);
        return await repo.CreateAsync(presupuesto, ct);
    }
}
