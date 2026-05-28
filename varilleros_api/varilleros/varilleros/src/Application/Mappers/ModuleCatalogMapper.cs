namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class ModuleCatalogMapper
{
    public static ModuleCatalogDto ToDto(this ModuleCatalog m) => new(
        m.Id, m.Code, m.Name, m.Description, m.IsActive);
}
