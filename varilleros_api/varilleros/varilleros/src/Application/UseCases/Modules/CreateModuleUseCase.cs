namespace Varilleros.src.Application.UseCases.Modules;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;

public sealed class CreateModuleUseCase(IModuleCatalogRepository repo)
{
    public async Task<int> ExecuteAsync(CreateModuleCatalogDto dto, CancellationToken ct = default)
    {
        var existing = await repo.GetByCodeAsync(dto.Code, ct);
        if (existing is not null)
            throw new Domain.Exceptions.DomainException($"Ya existe un módulo con código '{dto.Code}'.");

        var module = ModuleCatalog.Create(dto.Code, dto.Name, dto.Description);
        return await repo.CreateAsync(module, ct);
    }
}
