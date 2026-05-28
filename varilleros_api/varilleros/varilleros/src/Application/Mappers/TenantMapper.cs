namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class TenantMapper
{
    public static TenantDto ToDto(this Tenant t) => new(t.Id, t.Name, t.Slug, t.IsActive);
}
