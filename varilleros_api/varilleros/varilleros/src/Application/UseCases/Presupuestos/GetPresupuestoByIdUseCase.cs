namespace Varilleros.src.Application.UseCases.Presupuestos;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class GetPresupuestoByIdUseCase(IPresupuestoRepository repo)
{
    public async Task<PresupuestoDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var presupuesto = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Presupuesto), id);
        return presupuesto.ToDto();
    }
}
