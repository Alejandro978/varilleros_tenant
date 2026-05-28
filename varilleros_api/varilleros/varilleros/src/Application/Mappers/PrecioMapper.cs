namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class PrecioMapper
{
    public static PrecioDto ToDto(this Precio p) => new(
        p.NumeroabolladuraS,
        p.AletaLeve, p.AletaMedio, p.AletaGrave,
        p.AcristalaminaLeve, p.AcristalaminaMedio, p.AcristalaminaGrave,
        p.AletatrasLeve, p.AletatrasMedio, p.AletatrasGrave,
        p.AsientoLeve, p.AsientoMedio, p.AsientoGrave,
        p.AsientoDtraLeve, p.AsientoDtraMedio, p.AsientoDtraGrave,
        p.MaletaLeve, p.MaletaMedio, p.MaletaGrave);
}
