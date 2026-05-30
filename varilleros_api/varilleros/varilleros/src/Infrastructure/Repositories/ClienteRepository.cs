namespace Varilleros.src.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;
using Varilleros.src.Infrastructure.Data;

public sealed class ClienteRepository(TenantDbContext db) : IClienteRepository
{
    public async Task<Cliente?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await db.Clientes.FindAsync([id], ct);

    public async Task<IEnumerable<Cliente>> GetAllAsync(CancellationToken ct = default) =>
        await db.Clientes.OrderBy(c => c.Id).ToListAsync(ct);

    public async Task<int> CreateAsync(Cliente cliente, CancellationToken ct = default)
    {
        db.Clientes.Add(cliente);
        await db.SaveChangesAsync(ct);
        return cliente.Id;
    }

    public async Task UpdateAsync(Cliente cliente, CancellationToken ct = default) =>
        await db.SaveChangesAsync(ct);

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Clientes.FindAsync([id], ct);
        if (entity is not null)
        {
            db.Clientes.Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
}
