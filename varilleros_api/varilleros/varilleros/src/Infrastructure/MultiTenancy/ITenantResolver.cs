namespace Varilleros.src.Infrastructure.MultiTenancy;

public interface ITenantResolver
{
    Task<TenantContext> ResolveAsync(string slug, CancellationToken ct = default);
}
