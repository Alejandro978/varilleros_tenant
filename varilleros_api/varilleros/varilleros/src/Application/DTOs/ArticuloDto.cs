namespace Varilleros.src.Application.DTOs;

public record ArticuloDto(int Id, string Codigo, string Descripcion, string CodigoPrecioPresupuesto);

public record CreateArticuloDto(string Codigo, string Descripcion, string CodigoPrecioPresupuesto);

public record UpdateArticuloDto(int Id, string Codigo, string Descripcion, string CodigoPrecioPresupuesto);
