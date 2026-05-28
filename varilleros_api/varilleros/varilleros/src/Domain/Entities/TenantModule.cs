namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class TenantModule
{
    public int Id { get; private set; }
    public int TenantId { get; private set; }
    public int ModuleId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? GrantedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private TenantModule() { }

    public static TenantModule Create(int tenantId, int moduleId, DateTime? expiresAt = null)
    {
        if (tenantId <= 0)
            throw new DomainException("TenantId debe ser mayor a 0");
        if (moduleId <= 0)
            throw new DomainException("ModuleId debe ser mayor a 0");

        return new TenantModule
        {
            TenantId = tenantId,
            ModuleId = moduleId,
            IsActive = true,
            GrantedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Revoke()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reinstate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
