namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class ModuleCatalog
{
    public int Id { get; private set; }
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private ModuleCatalog() { }

    public static ModuleCatalog Create(string code, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new DomainException("Code es obligatorio");
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name es obligatorio");
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description es obligatoria");

        return new ModuleCatalog
        {
            Code = code,
            Name = name,
            Description = description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
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
