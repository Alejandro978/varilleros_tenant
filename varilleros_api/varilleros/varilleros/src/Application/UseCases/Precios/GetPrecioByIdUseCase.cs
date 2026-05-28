namespace Varilleros.src.Application.UseCases.Precios;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class GetPrecioByIdUseCase(IPreciosRepository repo)
{
    public async Task<PrecioDto> ExecuteAsync(int numeroabolladuras, CancellationToken ct = default)
    {
        var precio = await repo.GetByIdAsync(numeroabolladuras, ct)
            ?? throw new NotFoundException(nameof(Precio), numeroabolladuras);
        return precio.ToDto();
    }
}
