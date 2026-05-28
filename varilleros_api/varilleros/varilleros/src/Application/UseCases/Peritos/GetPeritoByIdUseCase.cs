namespace Varilleros.src.Application.UseCases.Peritos;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class GetPeritoByIdUseCase(IPeritosRepository repo)
{
    public async Task<PeritoDto> ExecuteAsync(long id, CancellationToken ct = default)
    {
        var perito = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Perito), id);
        return perito.ToDto();
    }
}
