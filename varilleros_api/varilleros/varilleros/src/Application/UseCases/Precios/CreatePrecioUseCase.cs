namespace Varilleros.src.Application.UseCases.Precios;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class CreatePrecioUseCase(IPreciosRepository repo)
{
    public async Task ExecuteAsync(CreatePrecioDto dto, CancellationToken ct = default)
    {
        var precio = Precio.Create(dto.NumeroabolladuraS);
        await repo.CreateAsync(precio, ct);
    }
}
