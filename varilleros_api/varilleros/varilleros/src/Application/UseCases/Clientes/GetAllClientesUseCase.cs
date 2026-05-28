namespace Varilleros.src.Application.UseCases.Clientes;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Repositories;

public sealed class GetAllClientesUseCase(IClienteRepository repo)
{
    public async Task<IEnumerable<ClienteDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var clientes = await repo.GetAllAsync(ct);
        return clientes.Select(c => c.ToDto());
    }
}
