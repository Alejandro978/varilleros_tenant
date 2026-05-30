namespace Varilleros.src.Application.DTOs;

public record PrecioDto(
    int  Numeroabolladuras,
    int? AletaLeve,  int? AletaMedio,  int? AletaGrave,
    int? PuertaLeve, int? PuertaMedio, int? PuertaGrave,
    int? TechoLeve,  int? TechoMedio,  int? TechoGrave,
    int? CapoLeve,   int? CapoMedio,   int? CapoGrave,
    int? PortonLeve, int? PortonMedio, int? PortonGrave,
    int? MontanteLeve, int? MontanteMedio, int? MontanteGrave);

public record CreatePrecioDto(int Numeroabolladuras);

public record UpdatePrecioDto(
    int  Numeroabolladuras,
    int? AletaLeve,  int? AletaMedio,  int? AletaGrave,
    int? PuertaLeve, int? PuertaMedio, int? PuertaGrave,
    int? TechoLeve,  int? TechoMedio,  int? TechoGrave,
    int? CapoLeve,   int? CapoMedio,   int? CapoGrave,
    int? PortonLeve, int? PortonMedio, int? PortonGrave,
    int? MontanteLeve, int? MontanteMedio, int? MontanteGrave);
