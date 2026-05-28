namespace Varilleros.src.Application.UseCases.Peritos;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories;

public sealed class GetAllPeritosUseCase(IPeritosRepository repo)
{
    public async Task<IEnumerable<PeritoDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var peritos = await repo.GetAllAsync(ct);
        return peritos.Select(p => p.ToDto());
    }
}
