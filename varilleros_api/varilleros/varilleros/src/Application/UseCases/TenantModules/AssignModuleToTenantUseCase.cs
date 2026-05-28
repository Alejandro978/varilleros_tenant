namespace Varilleros.src.Application.UseCases.TenantModules;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;

public sealed class AssignModuleToTenantUseCase(ITenantModuleRepository repo)
{
    public async Task<TenantModuleDto> ExecuteAsync(CreateTenantModuleDto dto, CancellationToken ct = default)
    {
        var tenantModule = TenantModule.Create(dto.TenantId, dto.ModuleId, dto.ExpiresAt);
        var id = await repo.CreateAsync(tenantModule, ct);
        var created = await repo.GetByIdAsync(id, ct);
        return created!.ToDto();
    }
}
