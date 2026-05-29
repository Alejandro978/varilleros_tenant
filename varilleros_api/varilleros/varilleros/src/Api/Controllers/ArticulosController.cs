namespace Varilleros.src.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Articulos;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class ArticulosController(
    GetAllArticulosUseCase getAll,
    GetArticuloByIdUseCase getById,
    CreateArticuloUseCase create,
    UpdateArticuloUseCase update,
    DeleteArticuloUseCase delete) : ControllerBase
{
    /// <summary>Listar todos los artículos</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await getAll.ExecuteAsync(ct));

    /// <summary>Obtener artículo por id</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) =>
        Ok(await getById.ExecuteAsync(id, ct));

    /// <summary>Crear nuevo artículo</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticuloDto dto, CancellationToken ct)
    {
        var id = await create.ExecuteAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Actualizar artículo</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateArticuloDto dto, CancellationToken ct)
    {
        await update.ExecuteAsync(id, dto with { Id = id }, ct);
        return NoContent();
    }

    /// <summary>Eliminar artículo</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await delete.ExecuteAsync(id, ct);
        return NoContent();
    }
}
