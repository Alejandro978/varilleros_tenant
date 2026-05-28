namespace Varilleros.src.Application.UseCases.Tenants;

using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories.Master;

public sealed class DeleteTenantUseCase(ITenantRepository repo)
{
    public async Task ExecuteAsync(int id, CancellationToken ct = default)
    {
        _ = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Tenant), id);
        await repo.DeleteAsync(id, ct);
    }
}
