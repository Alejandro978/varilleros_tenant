namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class PresupuestoMapper
{
    public static PresupuestoDto ToDto(this Presupuesto ps) => new(
        ps.Id, ps.ClienteId, ps.PeritoId, ps.FechaPresupuesto,
        ps.Descripcion, ps.TotalPresupuesto, ps.Estado);
}
