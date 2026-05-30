namespace Varilleros.src.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;

public sealed class TenantDbContext(DbContextOptions<TenantDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Perito> Peritos => Set<Perito>();
    public DbSet<Articulo> Articulos => Set<Articulo>();
    public DbSet<Precio> Precios => Set<Precio>();
    public DbSet<Presupuesto> Presupuestos => Set<Presupuesto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ── Cliente ──────────────────────────────────────────────────────────
        modelBuilder.Entity<Cliente>(e =>
        {
            e.ToTable("cliente");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.NombreCliente).HasColumnName("nombrecliente").HasMaxLength(200).IsRequired();
            e.Property(x => x.NifCif).HasColumnName("nifcif").HasMaxLength(20).IsRequired();
            e.Property(x => x.Direccion).HasColumnName("direccion").HasMaxLength(300).IsRequired();
            e.Property(x => x.Poblacion).HasColumnName("poblacion").HasMaxLength(150).IsRequired();
            e.Property(x => x.Email).HasColumnName("email").HasMaxLength(200).IsRequired();
            e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(20).IsRequired();
            e.Property(x => x.CreatedAt).HasColumnName("created_at");
            e.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        });

        // ── Perito ───────────────────────────────────────────────────────────
        modelBuilder.Entity<Perito>(e =>
        {
            e.ToTable("peritos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(200).IsRequired();
            e.Property(x => x.Email).HasColumnName("email").HasMaxLength(200).IsRequired();
            e.Property(x => x.CreatedAt).HasColumnName("created_at");
            e.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        });

        // ── Articulo ─────────────────────────────────────────────────────────
        modelBuilder.Entity<Articulo>(e =>
        {
            e.ToTable("articulo");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Codigo).HasColumnName("codigo").HasMaxLength(50).IsRequired();
            e.Property(x => x.Descripcion).HasColumnName("descripcion").HasMaxLength(500).IsRequired();
            e.Property(x => x.CodigoPrecioPresupuesto).HasColumnName("codigopreciopresupuesto").HasMaxLength(50).IsRequired();
            e.Property(x => x.CreatedAt).HasColumnName("created_at");
            e.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        });

        // ── Precio ───────────────────────────────────────────────────────────
        modelBuilder.Entity<Precio>(e =>
        {
            e.ToTable("precios");
            e.HasKey(x => x.Numeroabolladuras);
            e.Property(x => x.Numeroabolladuras).HasColumnName("numeroabolladuras").ValueGeneratedNever();
            e.Property(x => x.AletaLeve).HasColumnName("aletaleve");
            e.Property(x => x.AletaMedio).HasColumnName("aletamedio");
            e.Property(x => x.AletaGrave).HasColumnName("aletagrave");
            e.Property(x => x.PuertaLeve).HasColumnName("puertaleve");
            e.Property(x => x.PuertaMedio).HasColumnName("puertamedio");
            e.Property(x => x.PuertaGrave).HasColumnName("puertagrave");
            e.Property(x => x.TechoLeve).HasColumnName("techoleve");
            e.Property(x => x.TechoMedio).HasColumnName("techomedio");
            e.Property(x => x.TechoGrave).HasColumnName("techograve");
            e.Property(x => x.CapoLeve).HasColumnName("capoleve");
            e.Property(x => x.CapoMedio).HasColumnName("capomedio");
            e.Property(x => x.CapoGrave).HasColumnName("capograve");
            e.Property(x => x.PortonLeve).HasColumnName("portonleve");
            e.Property(x => x.PortonMedio).HasColumnName("portonmedio");
            e.Property(x => x.PortonGrave).HasColumnName("portongrave");
            e.Property(x => x.MontanteLeve).HasColumnName("montanteleve");
            e.Property(x => x.MontanteMedio).HasColumnName("montantemedio");
            e.Property(x => x.MontanteGrave).HasColumnName("montantegrave");
        });

        // ── Presupuesto ──────────────────────────────────────────────────────
        modelBuilder.Entity<Presupuesto>(e =>
        {
            e.ToTable("presupuesto");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Reparador).HasColumnName("reparador");
            e.Property(x => x.Marca).HasColumnName("marca");
            e.Property(x => x.Modelo).HasColumnName("modelo");
            e.Property(x => x.Matricula).HasColumnName("matricula");
            e.Property(x => x.PrecioTotal).HasColumnName("preciototal");
            // ADI
            e.Property(x => x.AdiLeve).HasColumnName("ADIleve");
            e.Property(x => x.AdiTotalDanyoLeve).HasColumnName("ADItotaldanyoleve");
            e.Property(x => x.AdiMedio).HasColumnName("ADImedio");
            e.Property(x => x.AdiTotalDanyoMedio).HasColumnName("ADItotaldanyomedio");
            e.Property(x => x.AdiGrave).HasColumnName("ADIgrave");
            e.Property(x => x.AdiTotalDanyoGrave).HasColumnName("ADItotaldanyograve");
            e.Property(x => x.AdiPintura).HasColumnName("ADIpintura");
            e.Property(x => x.AdiAluminio).HasColumnName("ADIaluminio");
            e.Property(x => x.AdiTotal).HasColumnName("ADItotal");
            // ADD
            e.Property(x => x.AddLeve).HasColumnName("ADDleve");
            e.Property(x => x.AddTotalDanyoLeve).HasColumnName("ADDtotaldanyoleve");
            e.Property(x => x.AddMedio).HasColumnName("ADDmedio");
            e.Property(x => x.AddTotalDanyoMedio).HasColumnName("ADDtotaldanyomedio");
            e.Property(x => x.AddGrave).HasColumnName("ADDgrave");
            e.Property(x => x.AddTotalDanyoGrave).HasColumnName("ADDtotaldanyograve");
            e.Property(x => x.AddPintura).HasColumnName("ADDpintura");
            e.Property(x => x.AddAluminio).HasColumnName("ADDaluminio");
            e.Property(x => x.AddTotal).HasColumnName("ADDtotal");
            // ATI
            e.Property(x => x.AtiLeve).HasColumnName("ATIleve");
            e.Property(x => x.AtiTotalDanyoLeve).HasColumnName("ATItotaldanyoleve");
            e.Property(x => x.AtiMedio).HasColumnName("ATImedio");
            e.Property(x => x.AtiTotalDanyoMedio).HasColumnName("ATItotaldanyomedio");
            e.Property(x => x.AtiGrave).HasColumnName("ATIgrave");
            e.Property(x => x.AtiTotalDanyoGrave).HasColumnName("ATItotaldanyograve");
            e.Property(x => x.AtiPintura).HasColumnName("ATIpintura");
            e.Property(x => x.AtiAluminio).HasColumnName("ATIaluminio");
            e.Property(x => x.AtiTotal).HasColumnName("ATItotal");
            // ATD
            e.Property(x => x.AtdLeve).HasColumnName("ATDleve");
            e.Property(x => x.AtdTotalDanyoLeve).HasColumnName("ATDtotaldanyoleve");
            e.Property(x => x.AtdMedio).HasColumnName("ATDmedio");
            e.Property(x => x.AtdTotalDanyoMedio).HasColumnName("ATDtotaldanyomedio");
            e.Property(x => x.AtdGrave).HasColumnName("ATDgrave");
            e.Property(x => x.AtdTotalDanyoGrave).HasColumnName("ATDtotaldanyograve");
            e.Property(x => x.AtdPintura).HasColumnName("ATDpintura");
            e.Property(x => x.AtdAluminio).HasColumnName("ATDaluminio");
            e.Property(x => x.AtdTotal).HasColumnName("ATDtotal");
            // PDI
            e.Property(x => x.PdiLeve).HasColumnName("PDIleve");
            e.Property(x => x.PdiTotalDanyoLeve).HasColumnName("PDItotaldanyoleve");
            e.Property(x => x.PdiMedio).HasColumnName("PDImedio");
            e.Property(x => x.PdiTotalDanyoMedio).HasColumnName("PDItotaldanyomedio");
            e.Property(x => x.PdiGrave).HasColumnName("PDIgrave");
            e.Property(x => x.PdiTotalDanyoGrave).HasColumnName("PDItotaldanyograve");
            e.Property(x => x.PdiPintura).HasColumnName("PDIpintura");
            e.Property(x => x.PdiAluminio).HasColumnName("PDIaluminio");
            e.Property(x => x.PdiTotal).HasColumnName("PDItotal");
            // PDD
            e.Property(x => x.PddLeve).HasColumnName("PDDleve");
            e.Property(x => x.PddTotalDanyoLeve).HasColumnName("PDDtotaldanyoleve");
            e.Property(x => x.PddMedio).HasColumnName("PDDmedio");
            e.Property(x => x.PddTotalDanyoMedio).HasColumnName("PDDtotaldanyomedio");
            e.Property(x => x.PddGrave).HasColumnName("PDDgrave");
            e.Property(x => x.PddTotalDanyoGrave).HasColumnName("PDDtotaldanyograve");
            e.Property(x => x.PddPintura).HasColumnName("PDDpintura");
            e.Property(x => x.PddAluminio).HasColumnName("PDDaluminio");
            e.Property(x => x.PddTotal).HasColumnName("PDDtotal");
            // PTD
            e.Property(x => x.PtdLeve).HasColumnName("PTDleve");
            e.Property(x => x.PtdTotalDanyoLeve).HasColumnName("PTDtotaldanyoleve");
            e.Property(x => x.PtdMedio).HasColumnName("PTDmedio");
            e.Property(x => x.PtdTotalDanyoMedio).HasColumnName("PTDtotaldanyomedio");
            e.Property(x => x.PtdGrave).HasColumnName("PTDgrave");
            e.Property(x => x.PtdTotalDanyoGrave).HasColumnName("PTDtotaldanyograve");
            e.Property(x => x.PtdPintura).HasColumnName("PTDpintura");
            e.Property(x => x.PtdAluminio).HasColumnName("PTDaluminio");
            e.Property(x => x.PtdTotal).HasColumnName("PTDtotal");
            // PTI
            e.Property(x => x.PtiLeve).HasColumnName("PTIleve");
            e.Property(x => x.PtiTotalDanyoLeve).HasColumnName("PTItotaldanyoleve");
            e.Property(x => x.PtiMedio).HasColumnName("PTImedio");
            e.Property(x => x.PtiTotalDanyoMedio).HasColumnName("PTItotaldanyomedio");
            e.Property(x => x.PtiGrave).HasColumnName("PTIgrave");
            e.Property(x => x.PtiTotalDanyoGrave).HasColumnName("PTItotaldanyograve");
            e.Property(x => x.PtiPintura).HasColumnName("PTIpintura");
            e.Property(x => x.PtiAluminio).HasColumnName("PTIaluminio");
            e.Property(x => x.PtiTotal).HasColumnName("PTItotal");
            // CAPO
            e.Property(x => x.CapoLeve).HasColumnName("CAPOleve");
            e.Property(x => x.CapoTotalDanyoLeve).HasColumnName("CAPOtotaldanyoleve");
            e.Property(x => x.CapoMedio).HasColumnName("CAPOmedio");
            e.Property(x => x.CapoTotalDanyoMedio).HasColumnName("CAPOtotaldanyomedio");
            e.Property(x => x.CapoGrave).HasColumnName("CAPOgrave");
            e.Property(x => x.CapoTotalDanyoGrave).HasColumnName("CAPOtotaldanyograve");
            e.Property(x => x.CapoPintura).HasColumnName("CAPOpintura");
            e.Property(x => x.CapoAluminio).HasColumnName("CAPOaluminio");
            e.Property(x => x.CapoTotal).HasColumnName("CAPOtotal");
            // TECHO
            e.Property(x => x.TechoLeve).HasColumnName("TECHOleve");
            e.Property(x => x.TechoTotalDanyoLeve).HasColumnName("TECHOtotaldanyoleve");
            e.Property(x => x.TechoMedio).HasColumnName("TECHOmedio");
            e.Property(x => x.TechoTotalDanyoMedio).HasColumnName("TECHOtotaldanyomedio");
            e.Property(x => x.TechoGrave).HasColumnName("TECHOgrave");
            e.Property(x => x.TechoTotalDanyoGrave).HasColumnName("TECHOtotaldanyograve");
            e.Property(x => x.TechoPintura).HasColumnName("TECHOpintura");
            e.Property(x => x.TechoAluminio).HasColumnName("TECHOaluminio");
            e.Property(x => x.TechoTotal).HasColumnName("TECHOtotal");
            // PORTON
            e.Property(x => x.PortonLeve).HasColumnName("PORTONleve");
            e.Property(x => x.PortonTotalDanyoLeve).HasColumnName("PORTONtotaldanyoleve");
            e.Property(x => x.PortonMedio).HasColumnName("PORTONmedio");
            e.Property(x => x.PortonTotalDanyoMedio).HasColumnName("PORTONtotaldanyomedio");
            e.Property(x => x.PortonGrave).HasColumnName("PORTONgrave");
            e.Property(x => x.PortonTotalDanyoGrave).HasColumnName("PORTONtotaldanyograve");
            e.Property(x => x.PortonPintura).HasColumnName("PORTONpintura");
            e.Property(x => x.PortonAluminio).HasColumnName("PORTONaluminio");
            e.Property(x => x.PortonTotal).HasColumnName("PORTONtotal");
            // MI
            e.Property(x => x.MiLeve).HasColumnName("MIleve");
            e.Property(x => x.MiTotalDanyoLeve).HasColumnName("MItotaldanyoleve");
            e.Property(x => x.MiMedio).HasColumnName("MImedio");
            e.Property(x => x.MiTotalDanyoMedio).HasColumnName("MItotaldanyomedio");
            e.Property(x => x.MiGrave).HasColumnName("MIgrave");
            e.Property(x => x.MiTotalDanyoGrave).HasColumnName("MItotaldanyograve");
            e.Property(x => x.MiPintura).HasColumnName("MIpintura");
            e.Property(x => x.MiAluminio).HasColumnName("MIaluminio");
            e.Property(x => x.MiTotal).HasColumnName("MItotal");
            // MD
            e.Property(x => x.MdLeve).HasColumnName("MDleve");
            e.Property(x => x.MdTotalDanyoLeve).HasColumnName("MDtotaldanyoleve");
            e.Property(x => x.MdMedio).HasColumnName("MDmedio");
            e.Property(x => x.MdTotalDanyoMedio).HasColumnName("MDtotaldanyomedio");
            e.Property(x => x.MdGrave).HasColumnName("MDgrave");
            e.Property(x => x.MdTotalDanyoGrave).HasColumnName("MDtotaldanyograve");
            e.Property(x => x.MdPintura).HasColumnName("MDpintura");
            e.Property(x => x.MdAluminio).HasColumnName("MDaluminio");
            e.Property(x => x.MdTotal).HasColumnName("MDtotal");
            // Control
            e.Property(x => x.FechaCreacion).HasColumnName("fechaCreacion");
            e.Property(x => x.Descuento).HasColumnName("descuento");
            e.Property(x => x.Observaciones).HasColumnName("observaciones");
            e.Property(x => x.Desmontajes).HasColumnName("desmontajes");
            e.Property(x => x.Estado).HasColumnName("estado");
            e.Property(x => x.NombreCliente).HasColumnName("nombrecliente");
            e.Property(x => x.Direccion).HasColumnName("direccion");
            e.Property(x => x.Poblacion).HasColumnName("poblacion");
            e.Property(x => x.NifCif).HasColumnName("nifcif");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.Telefono).HasColumnName("telefono");
            e.Property(x => x.Aseguradora).HasColumnName("aseguradora");
            e.Property(x => x.IdPerito).HasColumnName("idPerito");
        });
    }
}
