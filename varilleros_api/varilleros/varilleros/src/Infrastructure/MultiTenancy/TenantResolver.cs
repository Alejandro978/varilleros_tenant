namespace Varilleros.src.Infrastructure.MultiTenancy;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Infrastructure.Data;

public sealed class TenantResolver(
    IServiceScopeFactory scopeFactory,
    IMemoryCache cache,
    IOptions<TenantCacheOptions> options) : ITenantResolver
{
    public async Task<TenantContext> ResolveAsync(string slug, CancellationToken ct = default)
    {
        var cacheKey = $"tenant:{slug}";
        if (cache.TryGetValue(cacheKey, out TenantContext? cached))
            return cached!;

        // Crea un scope temporal para acceder a MasterDbContext (evita scoped-in-singleton)
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MasterDbContext>();

        var tenant = await db.Tenants
            .FirstOrDefaultAsync(t => t.Slug == slug && t.IsActive, ct)
            ?? throw new TenantNotFoundException(slug);

        var ctx = new TenantContext
        {
            Slug = tenant.Slug,
            DbConnStr = $"Server={tenant.DbHost};Port={tenant.DbPort};Database={tenant.DbName};User={tenant.DbUser};Password={tenant.DbPassword};CharSet=utf8mb4;"
        };

        cache.Set(cacheKey, ctx, options.Value.CacheDuration);
        return ctx;
    }
}

public sealed class TenantCacheOptions
{
    public int TtlSeconds { get; set; } = 300;
    public TimeSpan CacheDuration => TimeSpan.FromSeconds(TtlSeconds);
}
