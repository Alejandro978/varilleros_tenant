namespace Varilleros.src.Infrastructure.MultiTenancy;

using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Data;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories.Master;

public sealed class TenantResolver(
    IMasterDbConnectionFactory masterFactory,
    IMemoryCache cache,
    IOptions<TenantCacheOptions> options) : ITenantResolver
{
    public async Task<TenantContext> ResolveAsync(string slug, CancellationToken ct = default)
    {
        var cacheKey = $"tenant:{slug}";
        if (cache.TryGetValue(cacheKey, out TenantContext? cached))
            return cached!;

        using var conn = masterFactory.CreateConnection();
        var tenant = await conn.QuerySingleOrDefaultAsync<TenantRow>(
            "SELECT id, name, slug, db_host, db_port, db_name, db_user, db_password, is_active FROM tenants WHERE slug = @slug AND is_active = TRUE",
            new { slug }, commandTimeout: 30);

        if (tenant is null)
            throw new TenantNotFoundException(slug);

        var ctx = new TenantContext
        {
            Slug = tenant.Slug,
            DbConnStr = $"Server={tenant.DbHost};Port={tenant.DbPort};Database={tenant.DbName};User={tenant.DbUser};Password={tenant.DbPassword};CharSet=utf8mb4;"
        };

        cache.Set(cacheKey, ctx, options.Value.CacheDuration);
        return ctx;
    }

    private sealed record TenantRow(int Id, string Name, string Slug, string DbHost, int DbPort, string DbName, string DbUser, string DbPassword, bool IsActive);
}

public sealed class TenantCacheOptions
{
    public TimeSpan CacheDuration { get; set; } = TimeSpan.FromMinutes(5);
}
