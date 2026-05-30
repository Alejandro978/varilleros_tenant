namespace Varilleros.src.Application.UseCases.TenantModules;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories.Master;

public sealed class GetTenantModulesUseCase(
    ITenantModuleRepository repo,
    IModuleCatalogRepository moduleRepo)
{
    public async Task<IEnumerable<TenantModuleDto>> ExecuteAsync(int tenantId, CancellationToken ct = default)
    {
        var tenantModules = await repo.GetByTenantIdAsync(tenantId, ct);
        var allModules    = await moduleRepo.GetAllAsync(ct);
        var modulesDict   = allModules.ToDictionary(m => m.Id);

        return tenantModules.Select(tm =>
        {
            modulesDict.TryGetValue(tm.ModuleId, out var mod);
            // Devuelve todos los registros de tenant_module (activos e inactivos),
            // pero marca efectivamente inactivo si el catalog también lo está (interruptor maestro).
            var effectivelyActive = tm.IsActive && (mod?.IsActive ?? false);
            return tm.ToDto(
                mod?.Code ?? string.Empty,
                mod?.Name ?? string.Empty,
                effectivelyActive);
        });
    }
}
