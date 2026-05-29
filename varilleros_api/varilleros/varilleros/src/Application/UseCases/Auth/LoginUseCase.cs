namespace Varilleros.src.Application.UseCases.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DTOs;
using Varilleros.src.Domain.Repositories.Master;

public sealed class LoginUseCase(
    ITenantRepository tenantRepo,
    ITenantModuleRepository tenantModuleRepo,
    IModuleCatalogRepository moduleCatalogRepo,
    IConfiguration config)
{
    public async Task<LoginResponseDto?> ExecuteAsync(LoginDto dto, CancellationToken ct = default)
    {
        var tenant = await tenantRepo.GetBySlugAsync(dto.Slug, ct);
        if (tenant is null || !tenant.IsActive)
            return null;

        if (string.IsNullOrEmpty(tenant.PasswordHash))
            return null;

        if (!BCrypt.Verify(dto.Password, tenant.PasswordHash))
            return null;

        var token = GenerateToken(tenant.Id, tenant.Slug, tenant.Name);
        var modules = await GetActiveModulesAsync(tenant.Id, ct);

        return new LoginResponseDto(token, tenant.Name, tenant.Slug, modules);
    }

    private async Task<IEnumerable<ActiveModuleDto>> GetActiveModulesAsync(int tenantId, CancellationToken ct)
    {
        var tenantModules = await tenantModuleRepo.GetByTenantIdAsync(tenantId, ct);
        var active = tenantModules.Where(m => m.IsActive && (m.ExpiresAt == null || m.ExpiresAt > DateTime.UtcNow));

        var result = new List<ActiveModuleDto>();
        foreach (var tm in active)
        {
            var catalog = await moduleCatalogRepo.GetByIdAsync(tm.ModuleId, ct);
            if (catalog is { IsActive: true })
                result.Add(new ActiveModuleDto(catalog.Code, catalog.Name));
        }
        return result;
    }

    private string GenerateToken(int tenantId, string slug, string name)
    {
        var secret  = config["Jwt:Secret"]   ?? throw new InvalidOperationException("Jwt:Secret no configurado");
        var issuer  = config["Jwt:Issuer"]   ?? "varilleros-api";
        var audience = config["Jwt:Audience"] ?? "varilleros-app";
        var hours   = int.Parse(config["Jwt:ExpiryHours"] ?? "8");

        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, tenantId.ToString()),
            new Claim("slug",       slug),
            new Claim("tenantName", name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer:             issuer,
            audience:           audience,
            claims:             claims,
            expires:            DateTime.UtcNow.AddHours(hours),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
