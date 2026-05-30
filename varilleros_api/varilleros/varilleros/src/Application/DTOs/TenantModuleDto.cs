namespace Varilleros.src.Application.DTOs;

public record TenantModuleDto(
    int Id,
    int TenantId,
    int ModuleId,
    string ModuleCode,
    string ModuleName,
    bool IsActive,
    DateTime? GrantedAt,
    DateTime? ExpiresAt);

public record CreateTenantModuleDto(int TenantId, int ModuleId, DateTime? ExpiresAt = null);
