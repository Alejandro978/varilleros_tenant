namespace Varilleros.src.Application.UseCases.Tenants;

using BCrypt.Net;
using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories.Master;

public sealed class CreateTenantUseCase(ITenantRepository repo)
{
    public async Task<int> ExecuteAsync(CreateTenantDto dto, CancellationToken ct = default)
    {
        var tenant = Tenant.Create(
            dto.Name, dto.Slug, dto.DbHost, dto.DbPort,
            dto.DbName, dto.DbUser, dto.DbPassword);

        tenant.SetPasswordHash(BCrypt.HashPassword(dto.AppPassword, workFactor: 11));

        return await repo.CreateAsync(tenant, ct);
    }
}
