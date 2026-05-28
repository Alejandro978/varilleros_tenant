namespace Varilleros.src.Application.DTOs;

public record PrecioDto(
    int NumeroabolladuraS,
    int? AletaLeve, int? AletaMedio, int? AletaGrave,
    int? AcristalaminaLeve, int? AcristalaminaMedio, int? AcristalaminaGrave,
    int? AletatrasLeve, int? AletatrasMedio, int? AletatrasGrave,
    int? AsientoLeve, int? AsientoMedio, int? AsientoGrave,
    int? AsientoDtraLeve, int? AsientoDtraMedio, int? AsientoDtraGrave,
    int? MaletaLeve, int? MaletaMedio, int? MaletaGrave);

public record CreatePrecioDto(int NumeroabolladuraS);

public record UpdatePrecioDto(
    int NumeroabolladuraS,
    int? AletaLeve, int? AletaMedio, int? AletaGrave,
    int? AcristalaminaLeve, int? AcristalaminaMedio, int? AcristalaminaGrave,
    int? AletatrasLeve, int? AletatrasMedio, int? AletatrasGrave,
    int? AsientoLeve, int? AsientoMedio, int? AsientoGrave,
    int? AsientoDtraLeve, int? AsientoDtraMedio, int? AsientoDtraGrave,
    int? MaletaLeve, int? MaletaMedio, int? MaletaGrave);
