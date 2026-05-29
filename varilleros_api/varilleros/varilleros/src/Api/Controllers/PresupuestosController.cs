namespace Varilleros.src.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Presupuestos;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class PresupuestosController(
    GetAllPresupuestosUseCase getAll,
    GetPresupuestoByIdUseCase getById,
    CreatePresupuestoUseCase create,
    UpdatePresupuestoUseCase update,
    DeletePresupuestoUseCase delete) : ControllerBase
{
    /// <summary>Listar todos los presupuestos</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await getAll.ExecuteAsync(ct));

    /// <summary>Obtener presupuesto por id</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) =>
        Ok(await getById.ExecuteAsync(id, ct));

    /// <summary>Crear nuevo presupuesto</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePresupuestoDto dto, CancellationToken ct)
    {
        var id = await create.ExecuteAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Actualizar presupuesto</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePresupuestoDto dto, CancellationToken ct)
    {
        await update.ExecuteAsync(id, dto with { Id = id }, ct);
        return NoContent();
    }

    /// <summary>Eliminar presupuesto</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await delete.ExecuteAsync(id, ct);
        return NoContent();
    }
}
