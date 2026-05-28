namespace Varilleros.src.Api.Controllers.Admin;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Tenants;

[ApiController]
[Route("api/admin/tenants")]
public sealed class TenantsController(
    GetAllTenantsUseCase getAll,
    GetTenantByIdUseCase getById,
    CreateTenantUseCase create,
    UpdateTenantUseCase update,
    DeleteTenantUseCase delete) : ControllerBase
{
    /// <summary>Listar todos los tenants</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await getAll.ExecuteAsync(ct));

    /// <summary>Obtener tenant por id</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) =>
        Ok(await getById.ExecuteAsync(id, ct));

    /// <summary>Crear nuevo tenant</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenantDto dto, CancellationToken ct)
    {
        var id = await create.ExecuteAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Actualizar tenant</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTenantDto dto, CancellationToken ct)
    {
        await update.ExecuteAsync(id, dto with { Id = id }, ct);
        return NoContent();
    }

    /// <summary>Eliminar tenant</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await delete.ExecuteAsync(id, ct);
        return NoContent();
    }
}
