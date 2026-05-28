namespace Varilleros.src.Api.Controllers.Admin;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Modules;

[ApiController]
[Route("api/admin/modules")]
public sealed class ModulesCatalogController(
    GetAllModulesUseCase getAll,
    CreateModuleUseCase create) : ControllerBase
{
    /// <summary>Listar todos los módulos del catálogo</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await getAll.ExecuteAsync(ct));

    /// <summary>Crear nuevo módulo en el catálogo</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateModuleCatalogDto dto, CancellationToken ct)
    {
        var id = await create.ExecuteAsync(dto, ct);
        return CreatedAtAction(nameof(GetAll), new { id }, new { id });
    }
}
