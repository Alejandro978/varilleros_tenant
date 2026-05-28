namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class Precio
{
    public int NumeroabolladuraS { get; private set; }
    public int? AletaLeve { get; private set; }
    public int? AletaMedio { get; private set; }
    public int? AletaGrave { get; private set; }
    public int? AcristalaminaLeve { get; private set; }
    public int? AcristalaminaMedio { get; private set; }
    public int? AcristalaminaGrave { get; private set; }
    public int? AletatrasLeve { get; private set; }
    public int? AletatrasMedio { get; private set; }
    public int? AletatrasGrave { get; private set; }
    public int? AsientoLeve { get; private set; }
    public int? AsientoMedio { get; private set; }
    public int? AsientoGrave { get; private set; }
    public int? AsientoDtraLeve { get; private set; }
    public int? AsientoDtraMedio { get; private set; }
    public int? AsientoDtraGrave { get; private set; }
    public int? MaletaLeve { get; private set; }
    public int? MaletaMedio { get; private set; }
    public int? MaletaGrave { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Precio() { }

    public static Precio Create(int numeroabolladuras)
    {
        if (numeroabolladuras <= 0)
            throw new DomainException("NumeroabolladuraS debe ser mayor a 0");

        return new Precio
        {
            NumeroabolladuraS = numeroabolladuras,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        int? aletaLeve = null, int? aletaMedio = null, int? aletaGrave = null,
        int? acristalaminaLeve = null, int? acristalaminaMedio = null, int? acristalaminaGrave = null,
        int? aletatrasLeve = null, int? aletatrasMedio = null, int? aletatrasGrave = null,
        int? asientoLeve = null, int? asientoMedio = null, int? asientoGrave = null,
        int? asientoDtraLeve = null, int? asientoDtraMedio = null, int? asientoDtraGrave = null,
        int? maletaLeve = null, int? maletaMedio = null, int? maletaGrave = null)
    {
        AletaLeve = aletaLeve ?? AletaLeve;
        AletaMedio = aletaMedio ?? AletaMedio;
        AletaGrave = aletaGrave ?? AletaGrave;
        AcristalaminaLeve = acristalaminaLeve ?? AcristalaminaLeve;
        AcristalaminaMedio = acristalaminaMedio ?? AcristalaminaMedio;
        AcristalaminaGrave = acristalaminaGrave ?? AcristalaminaGrave;
        AletatrasLeve = aletatrasLeve ?? AletatrasLeve;
        AletatrasMedio = aletatrasMedio ?? AletatrasMedio;
        AletatrasGrave = aletatrasGrave ?? AletatrasGrave;
        AsientoLeve = asientoLeve ?? AsientoLeve;
        AsientoMedio = asientoMedio ?? AsientoMedio;
        AsientoGrave = asientoGrave ?? AsientoGrave;
        AsientoDtraLeve = asientoDtraLeve ?? AsientoDtraLeve;
        AsientoDtraMedio = asientoDtraMedio ?? AsientoDtraMedio;
        AsientoDtraGrave = asientoDtraGrave ?? AsientoDtraGrave;
        MaletaLeve = maletaLeve ?? MaletaLeve;
        MaletaMedio = maletaMedio ?? MaletaMedio;
        MaletaGrave = maletaGrave ?? MaletaGrave;
        UpdatedAt = DateTime.UtcNow;
    }
}
