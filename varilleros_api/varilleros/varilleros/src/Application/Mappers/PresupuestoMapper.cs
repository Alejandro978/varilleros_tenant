namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class PresupuestoMapper
{
    public static PresupuestoDto ToDto(this Presupuesto ps) => new(
        ps.Id, ps.Reparador, ps.Marca, ps.Modelo, ps.Matricula, ps.PrecioTotal,
        ps.AdiLeve, ps.AdiTotalDanyoLeve, ps.AdiMedio, ps.AdiTotalDanyoMedio,
        ps.AdiGrave, ps.AdiTotalDanyoGrave, ps.AdiPintura, ps.AdiAluminio, ps.AdiTotal,
        ps.AddLeve, ps.AddTotalDanyoLeve, ps.AddMedio, ps.AddTotalDanyoMedio,
        ps.AddGrave, ps.AddTotalDanyoGrave, ps.AddPintura, ps.AddAluminio, ps.AddTotal,
        ps.AtiLeve, ps.AtiTotalDanyoLeve, ps.AtiMedio, ps.AtiTotalDanyoMedio,
        ps.AtiGrave, ps.AtiTotalDanyoGrave, ps.AtiPintura, ps.AtiAluminio, ps.AtiTotal,
        ps.AtdLeve, ps.AtdTotalDanyoLeve, ps.AtdMedio, ps.AtdTotalDanyoMedio,
        ps.AtdGrave, ps.AtdTotalDanyoGrave, ps.AtdPintura, ps.AtdAluminio, ps.AtdTotal,
        ps.PdiLeve, ps.PdiTotalDanyoLeve, ps.PdiMedio, ps.PdiTotalDanyoMedio,
        ps.PdiGrave, ps.PdiTotalDanyoGrave, ps.PdiPintura, ps.PdiAluminio, ps.PdiTotal,
        ps.PddLeve, ps.PddTotalDanyoLeve, ps.PddMedio, ps.PddTotalDanyoMedio,
        ps.PddGrave, ps.PddTotalDanyoGrave, ps.PddPintura, ps.PddAluminio, ps.PddTotal,
        ps.PtdLeve, ps.PtdTotalDanyoLeve, ps.PtdMedio, ps.PtdTotalDanyoMedio,
        ps.PtdGrave, ps.PtdTotalDanyoGrave, ps.PtdPintura, ps.PtdAluminio, ps.PtdTotal,
        ps.PtiLeve, ps.PtiTotalDanyoLeve, ps.PtiMedio, ps.PtiTotalDanyoMedio,
        ps.PtiGrave, ps.PtiTotalDanyoGrave, ps.PtiPintura, ps.PtiAluminio, ps.PtiTotal,
        ps.CapoLeve, ps.CapoTotalDanyoLeve, ps.CapoMedio, ps.CapoTotalDanyoMedio,
        ps.CapoGrave, ps.CapoTotalDanyoGrave, ps.CapoPintura, ps.CapoAluminio, ps.CapoTotal,
        ps.TechoLeve, ps.TechoTotalDanyoLeve, ps.TechoMedio, ps.TechoTotalDanyoMedio,
        ps.TechoGrave, ps.TechoTotalDanyoGrave, ps.TechoPintura, ps.TechoAluminio, ps.TechoTotal,
        ps.PortonLeve, ps.PortonTotalDanyoLeve, ps.PortonMedio, ps.PortonTotalDanyoMedio,
        ps.PortonGrave, ps.PortonTotalDanyoGrave, ps.PortonPintura, ps.PortonAluminio, ps.PortonTotal,
        ps.MiLeve, ps.MiTotalDanyoLeve, ps.MiMedio, ps.MiTotalDanyoMedio,
        ps.MiGrave, ps.MiTotalDanyoGrave, ps.MiPintura, ps.MiAluminio, ps.MiTotal,
        ps.MdLeve, ps.MdTotalDanyoLeve, ps.MdMedio, ps.MdTotalDanyoMedio,
        ps.MdGrave, ps.MdTotalDanyoGrave, ps.MdPintura, ps.MdAluminio, ps.MdTotal,
        ps.FechaCreacion, ps.Descuento, ps.Observaciones, ps.Desmontajes, ps.Estado,
        ps.NombreCliente, ps.Direccion, ps.Poblacion, ps.NifCif,
        ps.Email, ps.Telefono, ps.Aseguradora, ps.IdPerito);
}
