namespace Varilleros.src.Application.UseCases.Peritos;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class CreatePeritoUseCase(IPeritosRepository repo)
{
    public async Task<long> ExecuteAsync(CreatePeritoDto dto, CancellationToken ct = default)
    {
        var perito = Perito.Create(dto.Nombre, dto.Email);
        return await repo.CreateAsync(perito, ct);
    }
}
