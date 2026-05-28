namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class Tenant
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string DbHost { get; private set; } = null!;
    public int DbPort { get; private set; }
    public string DbName { get; private set; } = null!;
    public string DbUser { get; private set; } = null!;
    public string DbPassword { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Tenant() { }

    public static Tenant Create(string name, string slug, string dbHost, int dbPort, string dbName, string dbUser, string dbPassword)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name es obligatorio");
        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException("Slug es obligatorio");
        if (string.IsNullOrWhiteSpace(dbHost))
            throw new DomainException("DbHost es obligatorio");
        if (dbPort <= 0)
            throw new DomainException("DbPort debe ser válido");
        if (string.IsNullOrWhiteSpace(dbName))
            throw new DomainException("DbName es obligatorio");
        if (string.IsNullOrWhiteSpace(dbUser))
            throw new DomainException("DbUser es obligatorio");
        if (string.IsNullOrWhiteSpace(dbPassword))
            throw new DomainException("DbPassword es obligatorio");

        return new Tenant
        {
            Name = name,
            Slug = slug,
            DbHost = dbHost,
            DbPort = dbPort,
            DbName = dbName,
            DbUser = dbUser,
            DbPassword = dbPassword,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string dbHost, int dbPort, string dbName, string dbUser, string dbPassword)
    {
        Name = name;
        DbHost = dbHost;
        DbPort = dbPort;
        DbName = dbName;
        DbUser = dbUser;
        DbPassword = dbPassword;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
