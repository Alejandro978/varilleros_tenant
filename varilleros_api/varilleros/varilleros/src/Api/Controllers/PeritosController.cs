namespace Varilleros.src.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Peritos;

[ApiController]
[Route("api/[controller]")]
public sealed class PeritosController(
    GetAllPeritosUseCase getAll,
    GetPeritoByIdUseCase getById,
    CreatePeritoUseCase create,
    UpdatePeritoUseCase update,
    DeletePeritoUseCase delete) : ControllerBase
{
    /// <summary>Listar todos los peritos</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await getAll.ExecuteAsync(ct));

    /// <summary>Obtener perito por id</summary>
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken ct) =>
        Ok(await getById.ExecuteAsync(id, ct));

    /// <summary>Crear nuevo perito</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePeritoDto dto, CancellationToken ct)
    {
        var id = await create.ExecuteAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Actualizar perito</summary>
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdatePeritoDto dto, CancellationToken ct)
    {
        await update.ExecuteAsync(id, dto with { Id = id }, ct);
        return NoContent();
    }

    /// <summary>Eliminar perito</summary>
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        await delete.ExecuteAsync(id, ct);
        return NoContent();
    }
}
