namespace Varilleros.src.Domain.Entities;

using Exceptions;

public sealed class Precio
{
    public int  Numeroabolladuras { get; private set; }
    public int? AletaLeve   { get; private set; }
    public int? AletaMedio  { get; private set; }
    public int? AletaGrave  { get; private set; }
    public int? PuertaLeve  { get; private set; }
    public int? PuertaMedio { get; private set; }
    public int? PuertaGrave { get; private set; }
    public int? TechoLeve   { get; private set; }
    public int? TechoMedio  { get; private set; }
    public int? TechoGrave  { get; private set; }
    public int? CapoLeve    { get; private set; }
    public int? CapoMedio   { get; private set; }
    public int? CapoGrave   { get; private set; }
    public int? PortonLeve  { get; private set; }
    public int? PortonMedio { get; private set; }
    public int? PortonGrave { get; private set; }
    public int? MontanteLeve   { get; private set; }
    public int? MontanteMedio  { get; private set; }
    public int? MontanteGrave  { get; private set; }

    private Precio() { }

    public static Precio Create(int numeroabolladuras)
    {
        if (numeroabolladuras <= 0)
            throw new DomainException("Numeroabolladuras debe ser mayor a 0");
        return new Precio { Numeroabolladuras = numeroabolladuras };
    }

    public void Update(
        int? aletaLeve   = null, int? aletaMedio   = null, int? aletaGrave   = null,
        int? puertaLeve  = null, int? puertaMedio  = null, int? puertaGrave  = null,
        int? techoLeve   = null, int? techoMedio   = null, int? techoGrave   = null,
        int? capoLeve    = null, int? capoMedio    = null, int? capoGrave    = null,
        int? portonLeve  = null, int? portonMedio  = null, int? portonGrave  = null,
        int? montanteLeve = null, int? montanteMedio = null, int? montanteGrave = null)
    {
        AletaLeve    = aletaLeve    ?? AletaLeve;
        AletaMedio   = aletaMedio   ?? AletaMedio;
        AletaGrave   = aletaGrave   ?? AletaGrave;
        PuertaLeve   = puertaLeve   ?? PuertaLeve;
        PuertaMedio  = puertaMedio  ?? PuertaMedio;
        PuertaGrave  = puertaGrave  ?? PuertaGrave;
        TechoLeve    = techoLeve    ?? TechoLeve;
        TechoMedio   = techoMedio   ?? TechoMedio;
        TechoGrave   = techoGrave   ?? TechoGrave;
        CapoLeve     = capoLeve     ?? CapoLeve;
        CapoMedio    = capoMedio    ?? CapoMedio;
        CapoGrave    = capoGrave    ?? CapoGrave;
        PortonLeve   = portonLeve   ?? PortonLeve;
        PortonMedio  = portonMedio  ?? PortonMedio;
        PortonGrave  = portonGrave  ?? PortonGrave;
        MontanteLeve  = montanteLeve  ?? MontanteLeve;
        MontanteMedio = montanteMedio ?? MontanteMedio;
        MontanteGrave = montanteGrave ?? MontanteGrave;
    }
}
