namespace Varilleros.src.Application.UseCases.TenantModules;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;

public sealed class AssignModuleToTenantUseCase(
    ITenantModuleRepository repo,
    IModuleCatalogRepository moduleRepo)
{
    public async Task<TenantModuleDto> ExecuteAsync(CreateTenantModuleDto dto, CancellationToken ct = default)
    {
        var tenantModule = TenantModule.Create(dto.TenantId, dto.ModuleId, dto.ExpiresAt);
        var id = await repo.CreateAsync(tenantModule, ct);
        var created = await repo.GetByIdAsync(id, ct);
        var mod = await moduleRepo.GetByIdAsync(dto.ModuleId, ct);
        return created!.ToDto(mod?.Code ?? string.Empty, mod?.Name ?? string.Empty);
    }
}
