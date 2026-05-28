namespace Varilleros.src.Application.UseCases.Peritos;

using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class DeletePeritoUseCase(IPeritosRepository repo)
{
    public async Task ExecuteAsync(long id, CancellationToken ct = default)
    {
        _ = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Perito), id);
        await repo.DeleteAsync(id, ct);
    }
}
