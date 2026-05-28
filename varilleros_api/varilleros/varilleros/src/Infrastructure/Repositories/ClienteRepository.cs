namespace Varilleros.src.Infrastructure.Repositories;

using Dapper;
using Data;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class ClienteRepository(ITenantDbConnectionFactory factory) : IClienteRepository
{
    public async Task<Cliente?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Cliente>(
            "SELECT * FROM cliente WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<Cliente>("SELECT * FROM cliente ORDER BY id");
    }

    public async Task<int> CreateAsync(Cliente cliente, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            INSERT INTO cliente (nombrecliente, nifcif, direccion, poblacion, email, telefono, created_at, updated_at)
            VALUES (@NombreCliente, @NifCif, @Direccion, @Poblacion, @Email, @Telefono, @CreatedAt, @UpdatedAt)
            """, cliente);
        return await conn.ExecuteScalarAsync<int>("SELECT LAST_INSERT_ID()");
    }

    public async Task UpdateAsync(Cliente cliente, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE cliente SET
                nombrecliente = @NombreCliente,
                nifcif = @NifCif,
                direccion = @Direccion,
                poblacion = @Poblacion,
                email = @Email,
                telefono = @Telefono,
                updated_at = @UpdatedAt
            WHERE id = @Id
            """, cliente);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM cliente WHERE id = @id", new { id });
    }
}
