namespace Varilleros.src.Application.DTOs;

public record ActiveModuleDto(string Code, string Name);

public record LoginResponseDto(
    string AccessToken,
    string TenantName,
    string Slug,
    int TenantId,
    IEnumerable<ActiveModuleDto> Modules);
