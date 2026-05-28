namespace Varilleros.src.Infrastructure.MultiTenancy;

public sealed class TenantContext
{
    public string Slug { get; init; } = default!;
    public string DbConnStr { get; init; } = default!;
}
