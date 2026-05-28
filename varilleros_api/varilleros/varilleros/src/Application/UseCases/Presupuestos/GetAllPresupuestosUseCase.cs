namespace Varilleros.src.Application.UseCases.Presupuestos;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories;

public sealed class GetAllPresupuestosUseCase(IPresupuestoRepository repo)
{
    public async Task<IEnumerable<PresupuestoDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var presupuestos = await repo.GetAllAsync(ct);
        return presupuestos.Select(ps => ps.ToDto());
    }
}
