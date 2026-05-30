namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class TenantModuleMapper
{
    /// <summary>
    /// effectiveIsActive = tenant_module.IsActive AND modules_catalog.IsActive
    /// Si el catálogo deshabilita el módulo, el flag efectivo es false.
    /// </summary>
    public static TenantModuleDto ToDto(
        this TenantModule tm,
        string moduleCode,
        string moduleName,
        bool? effectiveIsActive = null) => new(
            tm.Id, tm.TenantId, tm.ModuleId,
            moduleCode, moduleName,
            effectiveIsActive ?? tm.IsActive,
            tm.GrantedAt, tm.ExpiresAt);
}
