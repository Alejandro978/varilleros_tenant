namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class ArticuloMapper
{
    public static ArticuloDto ToDto(this Articulo a) => new(
        a.Id, a.Codigo, a.Descripcion, a.CodigoPrecioPresupuesto);
}
