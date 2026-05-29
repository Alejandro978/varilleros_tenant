namespace Varilleros.src.Api.Middleware;

using Infrastructure.MultiTenancy;

public sealed class TenantMiddleware(RequestDelegate next)
{
    // Las rutas de administración trabajan con la master DB y no requieren cabecera de tenant
    private static readonly string[] AdminPrefixes = ["/api/admin", "/api/auth", "/swagger", "/health"];

    public async Task InvokeAsync(HttpContext ctx, ITenantResolver resolver, TenantContextHolder holder)
    {
        // Saltar resolución de tenant para rutas de admin y utilidades
        var path = ctx.Request.Path.Value ?? string.Empty;
        if (AdminPrefixes.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
        {
            await next(ctx);
            return;
        }

        var slug = ctx.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(slug))
        {
            ctx.Response.StatusCode = 400;
            await ctx.Response.WriteAsJsonAsync(new { error = "Cabecera X-Tenant-Id requerida" });
            return;
        }

        try
        {
            var tenantCtx = await resolver.ResolveAsync(slug, ctx.RequestAborted);
            holder.Set(tenantCtx);
        }
        catch (Domain.Exceptions.TenantNotFoundException ex)
        {
            ctx.Response.StatusCode = 404;
            await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
            return;
        }

        await next(ctx);
    }
}
