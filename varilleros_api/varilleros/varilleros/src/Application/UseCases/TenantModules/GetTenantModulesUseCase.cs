namespace Varilleros.src.Application.UseCases.TenantModules;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories.Master;

public sealed class GetTenantModulesUseCase(ITenantModuleRepository repo)
{
    public async Task<IEnumerable<TenantModuleDto>> ExecuteAsync(int tenantId, CancellationToken ct = default)
    {
        var modules = await repo.GetByTenantIdAsync(tenantId, ct);
        return modules.Select(tm => tm.ToDto());
    }
}
