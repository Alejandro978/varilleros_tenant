namespace Varilleros.src.Application.UseCases.Precios;

using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class DeletePrecioUseCase(IPreciosRepository repo)
{
    public async Task ExecuteAsync(int numeroabolladuras, CancellationToken ct = default)
    {
        _ = await repo.GetByIdAsync(numeroabolladuras, ct)
            ?? throw new NotFoundException(nameof(Precio), numeroabolladuras);
        await repo.DeleteAsync(numeroabolladuras, ct);
    }
}
