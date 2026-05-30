namespace Varilleros.src.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Varilleros.src.Application.DTOs;
using Varilleros.src.Application.UseCases.Auth;
using Varilleros.src.Domain.Repositories.Master;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(
    LoginUseCase loginUseCase,
    ITenantModuleRepository tenantModuleRepo,
    IModuleCatalogRepository moduleRepo) : ControllerBase
{
    /// <summary>Login con slug de empresa y contraseña — devuelve JWT + módulos activos</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto, CancellationToken ct)
    {
        var result = await loginUseCase.ExecuteAsync(dto, ct);
        return result is null
            ? Unauthorized(new { error = "Empresa o contraseña incorrectos" })
            : Ok(result);
    }

    /// <summary>
    /// Refresca la lista de módulos activos del tenant sin hacer re-login.
    /// Aplica el doble filtro: tenant_module.is_active AND modules_catalog.is_active.
    /// </summary>
    [HttpGet("modules/{tenantId:int}")]
    public async Task<IActionResult> GetActiveModules(int tenantId, CancellationToken ct)
    {
        var modules = await BuildActiveModulesAsync(tenantId, ct);
        return Ok(modules);
    }

    private async Task<IEnumerable<ActiveModuleDto>> BuildActiveModulesAsync(int tenantId, CancellationToken ct)
    {
        var tenantModules = await tenantModuleRepo.GetByTenantIdAsync(tenantId, ct);
        var allModules    = await moduleRepo.GetAllAsync(ct);
        var modulesDict   = allModules.ToDictionary(m => m.Id);

        return tenantModules
            .Where(tm => tm.IsActive
                && modulesDict.TryGetValue(tm.ModuleId, out var mod)
                && mod!.IsActive)                                   // interruptor maestro del catalog
            .Select(tm => new ActiveModuleDto(
                modulesDict[tm.ModuleId].Code,
                modulesDict[tm.ModuleId].Name))
            .ToList();
    }
}
