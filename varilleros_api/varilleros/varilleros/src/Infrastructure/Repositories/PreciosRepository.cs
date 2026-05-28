namespace Varilleros.src.Infrastructure.Repositories;

using Dapper;
using Data;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class PreciosRepository(ITenantDbConnectionFactory factory) : IPreciosRepository
{
    public async Task<Precio?> GetByIdAsync(int numeroabolladuras, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Precio>(
            "SELECT * FROM precios WHERE numeroabolladuras = @numeroabolladuras", new { numeroabolladuras });
    }

    public async Task<IEnumerable<Precio>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<Precio>("SELECT * FROM precios ORDER BY numeroabolladuras");
    }

    public async Task CreateAsync(Precio precio, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            INSERT INTO precios (numeroabolladuras, aletaleve, aletamedio, aletagrave, acristalaminaLeve, acristalaminamedio, acristalaminagrave,
                aletatrasysleve, aletatrasymedio, aletatrasygrave, asientoleve, asientomedio, asientograve,
                asientodtraleve, asientodtramedio, asientodtragrave, maletaleve, maletamedio, maletagrave, created_at, updated_at)
            VALUES (@NumeroabolladuraS, @AletaLeve, @AletaMedio, @AletaGrave, @AcristalaminaLeve, @AcristalaminaMedio, @AcristalaminaGrave,
                @AletatrasLeve, @AletatrasMedio, @AletatrasGrave, @AsientoLeve, @AsientoMedio, @AsientoGrave,
                @AsientoDtraLeve, @AsientoDtraMedio, @AsientoDtraGrave, @MaletaLeve, @MaletaMedio, @MaletaGrave, @CreatedAt, @UpdatedAt)
            """, precio);
    }

    public async Task UpdateAsync(Precio precio, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE precios SET
                aletaleve = @AletaLeve,
                aletamedio = @AletaMedio,
                aletagrave = @AletaGrave,
                acristalaminaLeve = @AcristalaminaLeve,
                acristalaminamedio = @AcristalaminaMedio,
                acristalaminagrave = @AcristalaminaGrave,
                aletatrasysleve = @AletatrasLeve,
                aletatrasymedio = @AletatrasMedio,
                aletatrasygrave = @AletatrasGrave,
                asientoleve = @AsientoLeve,
                asientomedio = @AsientoMedio,
                asientograve = @AsientoGrave,
                asientodtraleve = @AsientoDtraLeve,
                asientodtramedio = @AsientoDtraMedio,
                asientodtragrave = @AsientoDtraGrave,
                maletaleve = @MaletaLeve,
                maletamedio = @MaletaMedio,
                maletagrave = @MaletaGrave,
                updated_at = @UpdatedAt
            WHERE numeroabolladuras = @NumeroabolladuraS
            """, precio);
    }

    public async Task DeleteAsync(int numeroabolladuras, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM precios WHERE numeroabolladuras = @numeroabolladuras", new { numeroabolladuras });
    }
}
