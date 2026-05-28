namespace Varilleros.src.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Clientes;

[ApiController]
[Route("api/[controller]")]
public sealed class ClientesController(
    GetAllClientesUseCase getAll,
    GetClienteByIdUseCase getById,
    CreateClienteUseCase create,
    UpdateClienteUseCase update,
    DeleteClienteUseCase delete) : ControllerBase
{
    /// <summary>Listar todos los clientes</summary>
    /// <remarks>Requiere cabecera X-Tenant-Id</remarks>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await getAll.ExecuteAsync(ct));

    /// <summary>Obtener cliente por id</summary>
    /// <param name="id">Id del cliente</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) =>
        Ok(await getById.ExecuteAsync(id, ct));

    /// <summary>Crear nuevo cliente</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClienteDto dto, CancellationToken ct)
    {
        var id = await create.ExecuteAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Actualizar cliente</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClienteDto dto, CancellationToken ct)
    {
        var updateDto = dto with { Id = id };
        await update.ExecuteAsync(id, updateDto, ct);
        return NoContent();
    }

    /// <summary>Eliminar cliente</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await delete.ExecuteAsync(id, ct);
        return NoContent();
    }
}
