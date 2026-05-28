namespace Varilleros.src.Application.UseCases.Articulos;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class CreateArticuloUseCase(IArticuloRepository repo)
{
    public async Task<int> ExecuteAsync(CreateArticuloDto dto, CancellationToken ct = default)
    {
        var articulo = Articulo.Create(dto.Codigo, dto.Descripcion, dto.CodigoPrecioPresupuesto);
        return await repo.CreateAsync(articulo, ct);
    }
}
