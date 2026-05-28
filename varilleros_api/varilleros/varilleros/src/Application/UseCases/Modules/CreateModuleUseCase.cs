namespace Varilleros.src.Application.UseCases.Modules;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;

public sealed class CreateModuleUseCase(IModuleCatalogRepository repo)
{
    public async Task<int> ExecuteAsync(CreateModuleCatalogDto dto, CancellationToken ct = default)
    {
        var module = ModuleCatalog.Create(dto.Code, dto.Name, dto.Description);
        return await repo.CreateAsync(module, ct);
    }
}
