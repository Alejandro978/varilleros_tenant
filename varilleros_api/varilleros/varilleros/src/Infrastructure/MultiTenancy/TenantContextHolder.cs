namespace Varilleros.src.Infrastructure.MultiTenancy;

public sealed class TenantContextHolder
{
    private TenantContext? _context;

    public void Set(TenantContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public TenantContext Get()
    {
        return _context ?? throw new InvalidOperationException("TenantContext no ha sido seteado.");
    }

    public bool IsSet => _context != null;
}
