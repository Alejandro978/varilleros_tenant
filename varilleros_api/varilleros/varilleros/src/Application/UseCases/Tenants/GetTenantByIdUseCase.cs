namespace Varilleros.src.Application.UseCases.Tenants;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories.Master;

public sealed class GetTenantByIdUseCase(ITenantRepository repo)
{
    public async Task<TenantDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var tenant = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Tenant), id);
        return tenant.ToDto();
    }
}
