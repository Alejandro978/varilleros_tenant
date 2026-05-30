namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class PrecioMapper
{
    public static PrecioDto ToDto(this Precio p) => new(
        p.Numeroabolladuras,
        p.AletaLeve,  p.AletaMedio,  p.AletaGrave,
        p.PuertaLeve, p.PuertaMedio, p.PuertaGrave,
        p.TechoLeve,  p.TechoMedio,  p.TechoGrave,
        p.CapoLeve,   p.CapoMedio,   p.CapoGrave,
        p.PortonLeve, p.PortonMedio, p.PortonGrave,
        p.MontanteLeve, p.MontanteMedio, p.MontanteGrave);
}
