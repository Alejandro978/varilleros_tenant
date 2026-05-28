namespace Varilleros.src.Application.UseCases.TenantModules;

using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories.Master;

public sealed class RevokeModuleFromTenantUseCase(ITenantModuleRepository repo)
{
    public async Task ExecuteAsync(int tenantId, int moduleId, CancellationToken ct = default)
    {
        var tm = await repo.GetByTenantAndModuleAsync(tenantId, moduleId, ct)
            ?? throw new NotFoundException("TenantModule", $"{tenantId}/{moduleId}");
        tm.Revoke();
        await repo.UpdateAsync(tm, ct);
    }
}
