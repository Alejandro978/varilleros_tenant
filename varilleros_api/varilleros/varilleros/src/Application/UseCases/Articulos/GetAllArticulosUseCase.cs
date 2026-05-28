namespace Varilleros.src.Application.UseCases.Articulos;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories;

public sealed class GetAllArticulosUseCase(IArticuloRepository repo)
{
    public async Task<IEnumerable<ArticuloDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var articulos = await repo.GetAllAsync(ct);
        return articulos.Select(a => a.ToDto());
    }
}
