namespace Varilleros.src.Application.DTOs;

public record PresupuestoDto(
    int Id,
    int ClienteId,
    int PeritoId,
    DateTime FechaPresupuesto,
    string Descripcion,
    decimal TotalPresupuesto,
    string Estado);

public record CreatePresupuestoDto(
    int ClienteId,
    int PeritoId,
    string Descripcion,
    decimal TotalPresupuesto);

public record UpdatePresupuestoDto(
    int Id,
    string Descripcion,
    decimal TotalPresupuesto,
    string Estado);
