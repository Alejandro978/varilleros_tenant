namespace Varilleros.src.Domain.Entities;

// Payload de escritura compartido por Create y Update
public record PresupuestoPayload(
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

public sealed class Presupuesto
{
    // ── Cabecera ──────────────────────────────────────────────────────────────
    public int     Id          { get; private set; }
    public string? Reparador   { get; private set; }
    public string? Marca       { get; private set; }
    public string? Modelo      { get; private set; }
    public string? Matricula   { get; private set; }
    public decimal? PrecioTotal { get; private set; }

    // ── Panel ADI ─────────────────────────────────────────────────────────────
    public decimal? AdiLeve            { get; private set; }
    public decimal? AdiTotalDanyoLeve  { get; private set; }
    public decimal? AdiMedio           { get; private set; }
    public decimal? AdiTotalDanyoMedio { get; private set; }
    public decimal? AdiGrave           { get; private set; }
    public decimal? AdiTotalDanyoGrave { get; private set; }
    public bool?    AdiPintura         { get; private set; }
    public bool?    AdiAluminio        { get; private set; }
    public decimal? AdiTotal           { get; private set; }

    // ── Panel ADD ─────────────────────────────────────────────────────────────
    public decimal? AddLeve            { get; private set; }
    public decimal? AddTotalDanyoLeve  { get; private set; }
    public decimal? AddMedio           { get; private set; }
    public decimal? AddTotalDanyoMedio { get; private set; }
    public decimal? AddGrave           { get; private set; }
    public decimal? AddTotalDanyoGrave { get; private set; }
    public bool?    AddPintura         { get; private set; }
    public bool?    AddAluminio        { get; private set; }
    public decimal? AddTotal           { get; private set; }

    // ── Panel ATI ─────────────────────────────────────────────────────────────
    public decimal? AtiLeve            { get; private set; }
    public decimal? AtiTotalDanyoLeve  { get; private set; }
    public decimal? AtiMedio           { get; private set; }
    public decimal? AtiTotalDanyoMedio { get; private set; }
    public decimal? AtiGrave           { get; private set; }
    public decimal? AtiTotalDanyoGrave { get; private set; }
    public bool?    AtiPintura         { get; private set; }
    public bool?    AtiAluminio        { get; private set; }
    public decimal? AtiTotal           { get; private set; }

    // ── Panel ATD ─────────────────────────────────────────────────────────────
    public decimal? AtdLeve            { get; private set; }
    public decimal? AtdTotalDanyoLeve  { get; private set; }
    public decimal? AtdMedio           { get; private set; }
    public decimal? AtdTotalDanyoMedio { get; private set; }
    public decimal? AtdGrave           { get; private set; }
    public decimal? AtdTotalDanyoGrave { get; private set; }
    public bool?    AtdPintura         { get; private set; }
    public bool?    AtdAluminio        { get; private set; }
    public decimal? AtdTotal           { get; private set; }

    // ── Panel PDI ─────────────────────────────────────────────────────────────
    public decimal? PdiLeve            { get; private set; }
    public decimal? PdiTotalDanyoLeve  { get; private set; }
    public decimal? PdiMedio           { get; private set; }
    public decimal? PdiTotalDanyoMedio { get; private set; }
    public decimal? PdiGrave           { get; private set; }
    public decimal? PdiTotalDanyoGrave { get; private set; }
    public bool?    PdiPintura         { get; private set; }
    public bool?    PdiAluminio        { get; private set; }
    public decimal? PdiTotal           { get; private set; }

    // ── Panel PDD ─────────────────────────────────────────────────────────────
    public decimal? PddLeve            { get; private set; }
    public decimal? PddTotalDanyoLeve  { get; private set; }
    public decimal? PddMedio           { get; private set; }
    public decimal? PddTotalDanyoMedio { get; private set; }
    public decimal? PddGrave           { get; private set; }
    public decimal? PddTotalDanyoGrave { get; private set; }
    public bool?    PddPintura         { get; private set; }
    public bool?    PddAluminio        { get; private set; }
    public decimal? PddTotal           { get; private set; }

    // ── Panel PTD ─────────────────────────────────────────────────────────────
    public decimal? PtdLeve            { get; private set; }
    public decimal? PtdTotalDanyoLeve  { get; private set; }
    public decimal? PtdMedio           { get; private set; }
    public decimal? PtdTotalDanyoMedio { get; private set; }
    public decimal? PtdGrave           { get; private set; }
    public decimal? PtdTotalDanyoGrave { get; private set; }
    public bool?    PtdPintura         { get; private set; }
    public bool?    PtdAluminio        { get; private set; }
    public decimal? PtdTotal           { get; private set; }

    // ── Panel PTI ─────────────────────────────────────────────────────────────
    public decimal? PtiLeve            { get; private set; }
    public decimal? PtiTotalDanyoLeve  { get; private set; }
    public decimal? PtiMedio           { get; private set; }
    public decimal? PtiTotalDanyoMedio { get; private set; }
    public decimal? PtiGrave           { get; private set; }
    public decimal? PtiTotalDanyoGrave { get; private set; }
    public bool?    PtiPintura         { get; private set; }
    public bool?    PtiAluminio        { get; private set; }
    public decimal? PtiTotal           { get; private set; }

    // ── Panel CAPO ────────────────────────────────────────────────────────────
    public decimal? CapoLeve            { get; private set; }
    public decimal? CapoTotalDanyoLeve  { get; private set; }
    public decimal? CapoMedio           { get; private set; }
    public decimal? CapoTotalDanyoMedio { get; private set; }
    public decimal? CapoGrave           { get; private set; }
    public decimal? CapoTotalDanyoGrave { get; private set; }
    public bool?    CapoPintura         { get; private set; }
    public bool?    CapoAluminio        { get; private set; }
    public decimal? CapoTotal           { get; private set; }

    // ── Panel TECHO ───────────────────────────────────────────────────────────
    public decimal? TechoLeve            { get; private set; }
    public decimal? TechoTotalDanyoLeve  { get; private set; }
    public decimal? TechoMedio           { get; private set; }
    public decimal? TechoTotalDanyoMedio { get; private set; }
    public decimal? TechoGrave           { get; private set; }
    public decimal? TechoTotalDanyoGrave { get; private set; }
    public bool?    TechoPintura         { get; private set; }
    public bool?    TechoAluminio        { get; private set; }
    public decimal? TechoTotal           { get; private set; }

    // ── Panel PORTON ──────────────────────────────────────────────────────────
    public decimal? PortonLeve            { get; private set; }
    public decimal? PortonTotalDanyoLeve  { get; private set; }
    public decimal? PortonMedio           { get; private set; }
    public decimal? PortonTotalDanyoMedio { get; private set; }
    public decimal? PortonGrave           { get; private set; }
    public decimal? PortonTotalDanyoGrave { get; private set; }
    public bool?    PortonPintura         { get; private set; }
    public bool?    PortonAluminio        { get; private set; }
    public decimal? PortonTotal           { get; private set; }

    // ── Panel MI ──────────────────────────────────────────────────────────────
    public decimal? MiLeve            { get; private set; }
    public decimal? MiTotalDanyoLeve  { get; private set; }
    public decimal? MiMedio           { get; private set; }
    public decimal? MiTotalDanyoMedio { get; private set; }
    public decimal? MiGrave           { get; private set; }
    public decimal? MiTotalDanyoGrave { get; private set; }
    public bool?    MiPintura         { get; private set; }
    public bool?    MiAluminio        { get; private set; }
    public decimal? MiTotal           { get; private set; }

    // ── Panel MD ──────────────────────────────────────────────────────────────
    public decimal? MdLeve            { get; private set; }
    public decimal? MdTotalDanyoLeve  { get; private set; }
    public decimal? MdMedio           { get; private set; }
    public decimal? MdTotalDanyoMedio { get; private set; }
    public decimal? MdGrave           { get; private set; }
    public decimal? MdTotalDanyoGrave { get; private set; }
    public bool?    MdPintura         { get; private set; }
    public bool?    MdAluminio        { get; private set; }
    public decimal? MdTotal           { get; private set; }

    // ── Control y cliente (datos desnormalizados) ─────────────────────────────
    public long?   FechaCreacion { get; private set; }
    public short?  Descuento     { get; private set; }
    public string? Observaciones { get; private set; }
    public short?  Desmontajes   { get; private set; }
    public short?  Estado        { get; private set; }
    public string? NombreCliente { get; private set; }
    public string? Direccion     { get; private set; }
    public string? Poblacion     { get; private set; }
    public string? NifCif        { get; private set; }
    public string? Email         { get; private set; }
    public string? Telefono      { get; private set; }
    public string? Aseguradora   { get; private set; }
    public long?   IdPerito      { get; private set; }

    private Presupuesto() { }

    public static Presupuesto Create(PresupuestoPayload d) => new()
    {
        Reparador = d.Reparador, Marca = d.Marca, Modelo = d.Modelo, Matricula = d.Matricula,
        PrecioTotal = d.PrecioTotal,
        AdiLeve = d.AdiLeve, AdiTotalDanyoLeve = d.AdiTotalDanyoLeve, AdiMedio = d.AdiMedio,
        AdiTotalDanyoMedio = d.AdiTotalDanyoMedio, AdiGrave = d.AdiGrave, AdiTotalDanyoGrave = d.AdiTotalDanyoGrave,
        AdiPintura = d.AdiPintura, AdiAluminio = d.AdiAluminio, AdiTotal = d.AdiTotal,
        AddLeve = d.AddLeve, AddTotalDanyoLeve = d.AddTotalDanyoLeve, AddMedio = d.AddMedio,
        AddTotalDanyoMedio = d.AddTotalDanyoMedio, AddGrave = d.AddGrave, AddTotalDanyoGrave = d.AddTotalDanyoGrave,
        AddPintura = d.AddPintura, AddAluminio = d.AddAluminio, AddTotal = d.AddTotal,
        AtiLeve = d.AtiLeve, AtiTotalDanyoLeve = d.AtiTotalDanyoLeve, AtiMedio = d.AtiMedio,
        AtiTotalDanyoMedio = d.AtiTotalDanyoMedio, AtiGrave = d.AtiGrave, AtiTotalDanyoGrave = d.AtiTotalDanyoGrave,
        AtiPintura = d.AtiPintura, AtiAluminio = d.AtiAluminio, AtiTotal = d.AtiTotal,
        AtdLeve = d.AtdLeve, AtdTotalDanyoLeve = d.AtdTotalDanyoLeve, AtdMedio = d.AtdMedio,
        AtdTotalDanyoMedio = d.AtdTotalDanyoMedio, AtdGrave = d.AtdGrave, AtdTotalDanyoGrave = d.AtdTotalDanyoGrave,
        AtdPintura = d.AtdPintura, AtdAluminio = d.AtdAluminio, AtdTotal = d.AtdTotal,
        PdiLeve = d.PdiLeve, PdiTotalDanyoLeve = d.PdiTotalDanyoLeve, PdiMedio = d.PdiMedio,
        PdiTotalDanyoMedio = d.PdiTotalDanyoMedio, PdiGrave = d.PdiGrave, PdiTotalDanyoGrave = d.PdiTotalDanyoGrave,
        PdiPintura = d.PdiPintura, PdiAluminio = d.PdiAluminio, PdiTotal = d.PdiTotal,
        PddLeve = d.PddLeve, PddTotalDanyoLeve = d.PddTotalDanyoLeve, PddMedio = d.PddMedio,
        PddTotalDanyoMedio = d.PddTotalDanyoMedio, PddGrave = d.PddGrave, PddTotalDanyoGrave = d.PddTotalDanyoGrave,
        PddPintura = d.PddPintura, PddAluminio = d.PddAluminio, PddTotal = d.PddTotal,
        PtdLeve = d.PtdLeve, PtdTotalDanyoLeve = d.PtdTotalDanyoLeve, PtdMedio = d.PtdMedio,
        PtdTotalDanyoMedio = d.PtdTotalDanyoMedio, PtdGrave = d.PtdGrave, PtdTotalDanyoGrave = d.PtdTotalDanyoGrave,
        PtdPintura = d.PtdPintura, PtdAluminio = d.PtdAluminio, PtdTotal = d.PtdTotal,
        PtiLeve = d.PtiLeve, PtiTotalDanyoLeve = d.PtiTotalDanyoLeve, PtiMedio = d.PtiMedio,
        PtiTotalDanyoMedio = d.PtiTotalDanyoMedio, PtiGrave = d.PtiGrave, PtiTotalDanyoGrave = d.PtiTotalDanyoGrave,
        PtiPintura = d.PtiPintura, PtiAluminio = d.PtiAluminio, PtiTotal = d.PtiTotal,
        CapoLeve = d.CapoLeve, CapoTotalDanyoLeve = d.CapoTotalDanyoLeve, CapoMedio = d.CapoMedio,
        CapoTotalDanyoMedio = d.CapoTotalDanyoMedio, CapoGrave = d.CapoGrave, CapoTotalDanyoGrave = d.CapoTotalDanyoGrave,
        CapoPintura = d.CapoPintura, CapoAluminio = d.CapoAluminio, CapoTotal = d.CapoTotal,
        TechoLeve = d.TechoLeve, TechoTotalDanyoLeve = d.TechoTotalDanyoLeve, TechoMedio = d.TechoMedio,
        TechoTotalDanyoMedio = d.TechoTotalDanyoMedio, TechoGrave = d.TechoGrave, TechoTotalDanyoGrave = d.TechoTotalDanyoGrave,
        TechoPintura = d.TechoPintura, TechoAluminio = d.TechoAluminio, TechoTotal = d.TechoTotal,
        PortonLeve = d.PortonLeve, PortonTotalDanyoLeve = d.PortonTotalDanyoLeve, PortonMedio = d.PortonMedio,
        PortonTotalDanyoMedio = d.PortonTotalDanyoMedio, PortonGrave = d.PortonGrave, PortonTotalDanyoGrave = d.PortonTotalDanyoGrave,
        PortonPintura = d.PortonPintura, PortonAluminio = d.PortonAluminio, PortonTotal = d.PortonTotal,
        MiLeve = d.MiLeve, MiTotalDanyoLeve = d.MiTotalDanyoLeve, MiMedio = d.MiMedio,
        MiTotalDanyoMedio = d.MiTotalDanyoMedio, MiGrave = d.MiGrave, MiTotalDanyoGrave = d.MiTotalDanyoGrave,
        MiPintura = d.MiPintura, MiAluminio = d.MiAluminio, MiTotal = d.MiTotal,
        MdLeve = d.MdLeve, MdTotalDanyoLeve = d.MdTotalDanyoLeve, MdMedio = d.MdMedio,
        MdTotalDanyoMedio = d.MdTotalDanyoMedio, MdGrave = d.MdGrave, MdTotalDanyoGrave = d.MdTotalDanyoGrave,
        MdPintura = d.MdPintura, MdAluminio = d.MdAluminio, MdTotal = d.MdTotal,
        FechaCreacion = d.FechaCreacion, Descuento = d.Descuento, Observaciones = d.Observaciones,
        Desmontajes = d.Desmontajes, Estado = d.Estado ?? 1,
        NombreCliente = d.NombreCliente, Direccion = d.Direccion, Poblacion = d.Poblacion,
        NifCif = d.NifCif, Email = d.Email, Telefono = d.Telefono,
        Aseguradora = d.Aseguradora, IdPerito = d.IdPerito,
    };

    public void Update(PresupuestoPayload d)
    {
        Reparador = d.Reparador; Marca = d.Marca; Modelo = d.Modelo; Matricula = d.Matricula;
        PrecioTotal = d.PrecioTotal;
        AdiLeve = d.AdiLeve; AdiTotalDanyoLeve = d.AdiTotalDanyoLeve; AdiMedio = d.AdiMedio;
        AdiTotalDanyoMedio = d.AdiTotalDanyoMedio; AdiGrave = d.AdiGrave; AdiTotalDanyoGrave = d.AdiTotalDanyoGrave;
        AdiPintura = d.AdiPintura; AdiAluminio = d.AdiAluminio; AdiTotal = d.AdiTotal;
        AddLeve = d.AddLeve; AddTotalDanyoLeve = d.AddTotalDanyoLeve; AddMedio = d.AddMedio;
        AddTotalDanyoMedio = d.AddTotalDanyoMedio; AddGrave = d.AddGrave; AddTotalDanyoGrave = d.AddTotalDanyoGrave;
        AddPintura = d.AddPintura; AddAluminio = d.AddAluminio; AddTotal = d.AddTotal;
        AtiLeve = d.AtiLeve; AtiTotalDanyoLeve = d.AtiTotalDanyoLeve; AtiMedio = d.AtiMedio;
        AtiTotalDanyoMedio = d.AtiTotalDanyoMedio; AtiGrave = d.AtiGrave; AtiTotalDanyoGrave = d.AtiTotalDanyoGrave;
        AtiPintura = d.AtiPintura; AtiAluminio = d.AtiAluminio; AtiTotal = d.AtiTotal;
        AtdLeve = d.AtdLeve; AtdTotalDanyoLeve = d.AtdTotalDanyoLeve; AtdMedio = d.AtdMedio;
        AtdTotalDanyoMedio = d.AtdTotalDanyoMedio; AtdGrave = d.AtdGrave; AtdTotalDanyoGrave = d.AtdTotalDanyoGrave;
        AtdPintura = d.AtdPintura; AtdAluminio = d.AtdAluminio; AtdTotal = d.AtdTotal;
        PdiLeve = d.PdiLeve; PdiTotalDanyoLeve = d.PdiTotalDanyoLeve; PdiMedio = d.PdiMedio;
        PdiTotalDanyoMedio = d.PdiTotalDanyoMedio; PdiGrave = d.PdiGrave; PdiTotalDanyoGrave = d.PdiTotalDanyoGrave;
        PdiPintura = d.PdiPintura; PdiAluminio = d.PdiAluminio; PdiTotal = d.PdiTotal;
        PddLeve = d.PddLeve; PddTotalDanyoLeve = d.PddTotalDanyoLeve; PddMedio = d.PddMedio;
        PddTotalDanyoMedio = d.PddTotalDanyoMedio; PddGrave = d.PddGrave; PddTotalDanyoGrave = d.PddTotalDanyoGrave;
        PddPintura = d.PddPintura; PddAluminio = d.PddAluminio; PddTotal = d.PddTotal;
        PtdLeve = d.PtdLeve; PtdTotalDanyoLeve = d.PtdTotalDanyoLeve; PtdMedio = d.PtdMedio;
        PtdTotalDanyoMedio = d.PtdTotalDanyoMedio; PtdGrave = d.PtdGrave; PtdTotalDanyoGrave = d.PtdTotalDanyoGrave;
        PtdPintura = d.PtdPintura; PtdAluminio = d.PtdAluminio; PtdTotal = d.PtdTotal;
        PtiLeve = d.PtiLeve; PtiTotalDanyoLeve = d.PtiTotalDanyoLeve; PtiMedio = d.PtiMedio;
        PtiTotalDanyoMedio = d.PtiTotalDanyoMedio; PtiGrave = d.PtiGrave; PtiTotalDanyoGrave = d.PtiTotalDanyoGrave;
        PtiPintura = d.PtiPintura; PtiAluminio = d.PtiAluminio; PtiTotal = d.PtiTotal;
        CapoLeve = d.CapoLeve; CapoTotalDanyoLeve = d.CapoTotalDanyoLeve; CapoMedio = d.CapoMedio;
        CapoTotalDanyoMedio = d.CapoTotalDanyoMedio; CapoGrave = d.CapoGrave; CapoTotalDanyoGrave = d.CapoTotalDanyoGrave;
        CapoPintura = d.CapoPintura; CapoAluminio = d.CapoAluminio; CapoTotal = d.CapoTotal;
        TechoLeve = d.TechoLeve; TechoTotalDanyoLeve = d.TechoTotalDanyoLeve; TechoMedio = d.TechoMedio;
        TechoTotalDanyoMedio = d.TechoTotalDanyoMedio; TechoGrave = d.TechoGrave; TechoTotalDanyoGrave = d.TechoTotalDanyoGrave;
        TechoPintura = d.TechoPintura; TechoAluminio = d.TechoAluminio; TechoTotal = d.TechoTotal;
        PortonLeve = d.PortonLeve; PortonTotalDanyoLeve = d.PortonTotalDanyoLeve; PortonMedio = d.PortonMedio;
        PortonTotalDanyoMedio = d.PortonTotalDanyoMedio; PortonGrave = d.PortonGrave; PortonTotalDanyoGrave = d.PortonTotalDanyoGrave;
        PortonPintura = d.PortonPintura; PortonAluminio = d.PortonAluminio; PortonTotal = d.PortonTotal;
        MiLeve = d.MiLeve; MiTotalDanyoLeve = d.MiTotalDanyoLeve; MiMedio = d.MiMedio;
        MiTotalDanyoMedio = d.MiTotalDanyoMedio; MiGrave = d.MiGrave; MiTotalDanyoGrave = d.MiTotalDanyoGrave;
        MiPintura = d.MiPintura; MiAluminio = d.MiAluminio; MiTotal = d.MiTotal;
        MdLeve = d.MdLeve; MdTotalDanyoLeve = d.MdTotalDanyoLeve; MdMedio = d.MdMedio;
        MdTotalDanyoMedio = d.MdTotalDanyoMedio; MdGrave = d.MdGrave; MdTotalDanyoGrave = d.MdTotalDanyoGrave;
        MdPintura = d.MdPintura; MdAluminio = d.MdAluminio; MdTotal = d.MdTotal;
        FechaCreacion = d.FechaCreacion; Descuento = d.Descuento; Observaciones = d.Observaciones;
        Desmontajes = d.Desmontajes; Estado = d.Estado ?? Estado;
        NombreCliente = d.NombreCliente; Direccion = d.Direccion; Poblacion = d.Poblacion;
        NifCif = d.NifCif; Email = d.Email; Telefono = d.Telefono;
        Aseguradora = d.Aseguradora; IdPerito = d.IdPerito;
    }
}
