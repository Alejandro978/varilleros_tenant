namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class Cliente
{
    public int Id { get; private set; }
    public string NombreCliente { get; private set; } = null!;
    public string NifCif { get; private set; } = null!;
    public string Direccion { get; private set; } = null!;
    public string Poblacion { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Telefono { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Cliente() { }

    public static Cliente Create(
        string nombreCliente, string nifCif,
        string direccion, string poblacion,
        string email, string telefono)
    {
        if (string.IsNullOrWhiteSpace(nombreCliente))
            throw new DomainException("NombreCliente es obligatorio");
        if (string.IsNullOrWhiteSpace(nifCif))
            throw new DomainException("NifCif es obligatorio");
        if (string.IsNullOrWhiteSpace(direccion))
            throw new DomainException("Dirección es obligatoria");
        if (string.IsNullOrWhiteSpace(poblacion))
            throw new DomainException("Población es obligatoria");
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email es obligatorio");
        if (string.IsNullOrWhiteSpace(telefono))
            throw new DomainException("Teléfono es obligatorio");

        return new Cliente
        {
            NombreCliente = nombreCliente,
            NifCif = nifCif,
            Direccion = direccion,
            Poblacion = poblacion,
            Email = email,
            Telefono = telefono,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string nombreCliente, string nifCif,
        string direccion, string poblacion,
        string email, string telefono)
    {
        NombreCliente = nombreCliente;
        NifCif = nifCif;
        Direccion = direccion;
        Poblacion = poblacion;
        Email = email;
        Telefono = telefono;
        UpdatedAt = DateTime.UtcNow;
    }
}
