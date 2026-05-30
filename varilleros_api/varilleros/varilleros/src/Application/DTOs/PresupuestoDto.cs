namespace Varilleros.src.Application.DTOs;

using Varilleros.src.Domain.Entities;

// PresupuestoPayload (create/update input) se define en Domain.Entities.PresupuestoPayload
// y se usa directamente en controllers y use cases.

public record PresupuestoDto(
    int Id,
    string? Reparador, string? Marca, string? Modelo, string? Matricula,
    decimal? PrecioTotal,
    decimal? AdiLeve, decimal? AdiTotalDanyoLeve, decimal? AdiMedio, decimal? AdiTotalDanyoMedio,
    decimal? AdiGrave, decimal? AdiTotalDanyoGrave, bool? AdiPintura, bool? AdiAluminio, decimal? AdiTotal,
    decimal? AddLeve, decimal? AddTotalDanyoLeve, decimal? AddMedio, decimal? AddTotalDanyoMedio,
    decimal? AddGrave, decimal? AddTotalDanyoGrave, bool? AddPintura, bool? AddAluminio, decimal? AddTotal,
    decimal? AtiLeve, decimal? AtiTotalDanyoLeve, decimal? AtiMedio, decimal? AtiTotalDanyoMedio,
    decimal? AtiGrave, decimal? AtiTotalDanyoGrave, bool? AtiPintura, bool? AtiAluminio, decimal? AtiTotal,
    decimal? AtdLeve, decimal? AtdTotalDanyoLeve, decimal? AtdMedio, decimal? AtdTotalDanyoMedio,
    decimal? AtdGrave, decimal? AtdTotalDanyoGrave, bool? AtdPintura, bool? AtdAluminio, decimal? AtdTotal,
    decimal? PdiLeve, decimal? PdiTotalDanyoLeve, decimal? PdiMedio, decimal? PdiTotalDanyoMedio,
    decimal? PdiGrave, decimal? PdiTotalDanyoGrave, bool? PdiPintura, bool? PdiAluminio, decimal? PdiTotal,
    decimal? PddLeve, decimal? PddTotalDanyoLeve, decimal? PddMedio, decimal? PddTotalDanyoMedio,
    decimal? PddGrave, decimal? PddTotalDanyoGrave, bool? PddPintura, bool? PddAluminio, decimal? PddTotal,
    decimal? PtdLeve, decimal? PtdTotalDanyoLeve, decimal? PtdMedio, decimal? PtdTotalDanyoMedio,
    decimal? PtdGrave, decimal? PtdTotalDanyoGrave, bool? PtdPintura, bool? PtdAluminio, decimal? PtdTotal,
    decimal? PtiLeve, decimal? PtiTotalDanyoLeve, decimal? PtiMedio, decimal? PtiTotalDanyoMedio,
    decimal? PtiGrave, decimal? PtiTotalDanyoGrave, bool? PtiPintura, bool? PtiAluminio, decimal? PtiTotal,
    decimal? CapoLeve, decimal? CapoTotalDanyoLeve, decimal? CapoMedio, decimal? CapoTotalDanyoMedio,
    decimal? CapoGrave, decimal? CapoTotalDanyoGrave, bool? CapoPintura, bool? CapoAluminio, decimal? CapoTotal,
    decimal? TechoLeve, decimal? TechoTotalDanyoLeve, decimal? TechoMedio, decimal? TechoTotalDanyoMedio,
    decimal? TechoGrave, decimal? TechoTotalDanyoGrave, bool? TechoPintura, bool? TechoAluminio, decimal? TechoTotal,
    decimal? PortonLeve, decimal? PortonTotalDanyoLeve, decimal? PortonMedio, decimal? PortonTotalDanyoMedio,
    decimal? PortonGrave, decimal? PortonTotalDanyoGrave, bool? PortonPintura, bool? PortonAluminio, decimal? PortonTotal,
    decimal? MiLeve, decimal? MiTotalDanyoLeve, decimal? MiMedio, decimal? MiTotalDanyoMedio,
    decimal? MiGrave, decimal? MiTotalDanyoGrave, bool? MiPintura, bool? MiAluminio, decimal? MiTotal,
    decimal? MdLeve, decimal? MdTotalDanyoLeve, decimal? MdMedio, decimal? MdTotalDanyoMedio,
    decimal? MdGrave, decimal? MdTotalDanyoGrave, bool? MdPintura, bool? MdAluminio, decimal? MdTotal,
    long? FechaCreacion, short? Descuento, string? Observaciones, short? Desmontajes, short? Estado,
    string? NombreCliente, string? Direccion, string? Poblacion, string? NifCif,
    string? Email, string? Telefono, string? Aseguradora, long? IdPerito);
