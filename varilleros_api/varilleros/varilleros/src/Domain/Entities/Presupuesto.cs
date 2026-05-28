namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class Presupuesto
{
    public int Id { get; private set; }
    public int ClienteId { get; private set; }
    public int PeritoId { get; private set; }
    public DateTime FechaPresupuesto { get; private set; }
    public string Descripcion { get; private set; } = null!;
    public decimal TotalPresupuesto { get; private set; }
    public string Estado { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Presupuesto() { }

    public static Presupuesto Create(int clienteId, int peritoId, string descripcion, decimal totalPresupuesto, string estado = "Pendiente")
    {
        if (clienteId <= 0)
            throw new DomainException("ClienteId debe ser mayor a 0");
        if (peritoId <= 0)
            throw new DomainException("PeritoId debe ser mayor a 0");
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("Descripción es obligatoria");
        if (totalPresupuesto < 0)
            throw new DomainException("Total del presupuesto no puede ser negativo");
        if (string.IsNullOrWhiteSpace(estado))
            throw new DomainException("Estado es obligatorio");

        return new Presupuesto
        {
            ClienteId = clienteId,
            PeritoId = peritoId,
            FechaPresupuesto = DateTime.UtcNow,
            Descripcion = descripcion,
            TotalPresupuesto = totalPresupuesto,
            Estado = estado,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string descripcion, decimal totalPresupuesto, string estado)
    {
        Descripcion = descripcion;
        TotalPresupuesto = totalPresupuesto;
        Estado = estado;
        UpdatedAt = DateTime.UtcNow;
    }
}
