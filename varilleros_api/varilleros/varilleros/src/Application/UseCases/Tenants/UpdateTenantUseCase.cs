namespace Varilleros.src.Application.UseCases.Tenants;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories.Master;

public sealed class UpdateTenantUseCase(ITenantRepository repo)
{
    public async Task ExecuteAsync(int id, UpdateTenantDto dto, CancellationToken ct = default)
    {
        var tenant = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Tenant), id);
        tenant.Update(dto.Name, dto.DbHost, dto.DbPort, dto.DbName, dto.DbUser, dto.DbPassword);
        await repo.UpdateAsync(tenant, ct);
    }
}
