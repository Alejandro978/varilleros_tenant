namespace Varilleros.src.Application.UseCases.Articulos;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class GetArticuloByIdUseCase(IArticuloRepository repo)
{
    public async Task<ArticuloDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var articulo = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Articulo), id);
        return articulo.ToDto();
    }
}
