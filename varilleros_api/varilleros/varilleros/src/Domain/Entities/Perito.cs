namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class Perito
{
    public long Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Perito() { }

    public static Perito Create(string nombre, string email)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("Nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email es obligatorio");

        return new Perito
        {
            Nombre = nombre,
            Email = email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string nombre, string email)
    {
        Nombre = nombre;
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }
}
