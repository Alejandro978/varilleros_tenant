namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class Articulo
{
    public int Id { get; private set; }
    public string Codigo { get; private set; } = null!;
    public string Descripcion { get; private set; } = null!;
    public string CodigoPrecioPresupuesto { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Articulo() { }

    public static Articulo Create(string codigo, string descripcion, string codigoPrecioPresupuesto)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new DomainException("Código es obligatorio");
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("Descripción es obligatoria");
        if (string.IsNullOrWhiteSpace(codigoPrecioPresupuesto))
            throw new DomainException("CodigoPrecioPresupuesto es obligatorio");

        return new Articulo
        {
            Codigo = codigo,
            Descripcion = descripcion,
            CodigoPrecioPresupuesto = codigoPrecioPresupuesto,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string codigo, string descripcion, string codigoPrecioPresupuesto)
    {
        Codigo = codigo;
        Descripcion = descripcion;
        CodigoPrecioPresupuesto = codigoPrecioPresupuesto;
        UpdatedAt = DateTime.UtcNow;
    }
}
