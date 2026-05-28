namespace Varilleros.src.Api.Controllers.Admin;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.TenantModules;

[ApiController]
[Route("api/admin/tenants/{tenantId:int}/modules")]
public sealed class TenantModulesController(
    GetTenantModulesUseCase getModules,
    AssignModuleToTenantUseCase assign,
    RevokeModuleFromTenantUseCase revoke) : ControllerBase
{
    /// <summary>Listar módulos activos de un tenant</summary>
    [HttpGet]
    public async Task<IActionResult> GetModules(int tenantId, CancellationToken ct) =>
        Ok(await getModules.ExecuteAsync(tenantId, ct));

    /// <summary>Asignar módulo a un tenant</summary>
    [HttpPost("{moduleId:int}")]
    public async Task<IActionResult> Assign(int tenantId, int moduleId, CancellationToken ct)
    {
        var dto = new CreateTenantModuleDto(tenantId, moduleId);
        var result = await assign.ExecuteAsync(dto, ct);
        return Ok(result);
    }

    /// <summary>Revocar módulo de un tenant</summary>
    [HttpDelete("{moduleId:int}")]
    public async Task<IActionResult> Revoke(int tenantId, int moduleId, CancellationToken ct)
    {
        await revoke.ExecuteAsync(tenantId, moduleId, ct);
        return NoContent();
    }
}
