namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class TenantModuleMapper
{
    public static TenantModuleDto ToDto(this TenantModule tm) => new(
        tm.Id, tm.TenantId, tm.ModuleId, tm.IsActive, tm.GrantedAt, tm.ExpiresAt);
}
