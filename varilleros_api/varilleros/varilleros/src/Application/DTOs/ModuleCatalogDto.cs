namespace Varilleros.src.Application.DTOs;

public record ModuleCatalogDto(int Id, string Code, string Name, string Description, bool IsActive);

public record CreateModuleCatalogDto(string Code, string Name, string Description);

public record UpdateModuleCatalogDto(int Id, string Name, string Description);
