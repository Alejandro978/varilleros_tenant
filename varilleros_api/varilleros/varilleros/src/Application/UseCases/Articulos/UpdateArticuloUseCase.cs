namespace Varilleros.src.Application.UseCases.Articulos;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class UpdateArticuloUseCase(IArticuloRepository repo)
{
    public async Task ExecuteAsync(int id, UpdateArticuloDto dto, CancellationToken ct = default)
    {
        var articulo = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Articulo), id);
        articulo.Update(dto.Codigo, dto.Descripcion, dto.CodigoPrecioPresupuesto);
        await repo.UpdateAsync(articulo, ct);
    }
}
