namespace Varilleros.src.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Precios;

[ApiController]
[Route("api/[controller]")]
public sealed class PreciosController(
    GetAllPreciosUseCase getAll,
    GetPrecioByIdUseCase getById,
    CreatePrecioUseCase create,
    UpdatePrecioUseCase update,
    DeletePrecioUseCase delete) : ControllerBase
{
    /// <summary>Listar todos los precios</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await getAll.ExecuteAsync(ct));

    /// <summary>Obtener precio por número de abolladuras</summary>
    [HttpGet("{numeroabolladuras:int}")]
    public async Task<IActionResult> GetById(int numeroabolladuras, CancellationToken ct) =>
        Ok(await getById.ExecuteAsync(numeroabolladuras, ct));

    /// <summary>Crear nueva fila de precio</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePrecioDto dto, CancellationToken ct)
    {
        await create.ExecuteAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { numeroabolladuras = dto.NumeroabolladuraS }, null);
    }

    /// <summary>Actualizar precio</summary>
    [HttpPut("{numeroabolladuras:int}")]
    public async Task<IActionResult> Update(int numeroabolladuras, [FromBody] UpdatePrecioDto dto, CancellationToken ct)
    {
        await update.ExecuteAsync(numeroabolladuras, dto with { NumeroabolladuraS = numeroabolladuras }, ct);
        return NoContent();
    }

    /// <summary>Eliminar precio</summary>
    [HttpDelete("{numeroabolladuras:int}")]
    public async Task<IActionResult> Delete(int numeroabolladuras, CancellationToken ct)
    {
        await delete.ExecuteAsync(numeroabolladuras, ct);
        return NoContent();
    }
}
