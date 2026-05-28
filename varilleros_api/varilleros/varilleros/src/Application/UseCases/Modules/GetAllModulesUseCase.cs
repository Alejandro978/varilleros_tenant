namespace Varilleros.src.Application.UseCases.Modules;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories.Master;

public sealed class GetAllModulesUseCase(IModuleCatalogRepository repo)
{
    public async Task<IEnumerable<ModuleCatalogDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var modules = await repo.GetAllAsync(ct);
        return modules.Select(m => m.ToDto());
    }
}
