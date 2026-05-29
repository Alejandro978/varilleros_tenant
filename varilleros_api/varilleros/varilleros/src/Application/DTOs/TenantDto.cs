namespace Varilleros.src.Application.DTOs;

public record TenantDto(int Id, string Name, string Slug, bool IsActive);

public record CreateTenantDto(
    string Name,
    string Slug,
    string DbHost,
    int DbPort,
    string DbName,
    string DbUser,
    string DbPassword,
    string AppPassword);

public record UpdateTenantDto(
    int Id,
    string Name,
    string DbHost,
    int DbPort,
    string DbName,
    string DbUser,
    string DbPassword);

public record SetTenantPasswordDto(string NewPassword);
