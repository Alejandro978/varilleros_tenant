namespace Varilleros.src.Application.UseCases.Precios;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories;

public sealed class GetAllPreciosUseCase(IPreciosRepository repo)
{
    public async Task<IEnumerable<PrecioDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var precios = await repo.GetAllAsync(ct);
        return precios.Select(p => p.ToDto());
    }
}
