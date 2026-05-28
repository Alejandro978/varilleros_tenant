namespace Varilleros.src.Application.UseCases.Tenants;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories.Master;

public sealed class GetAllTenantsUseCase(ITenantRepository repo)
{
    public async Task<IEnumerable<TenantDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var tenants = await repo.GetAllAsync(ct);
        return tenants.Select(t => t.ToDto());
    }
}
