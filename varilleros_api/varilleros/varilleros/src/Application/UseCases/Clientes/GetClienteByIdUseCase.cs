namespace Varilleros.src.Application.UseCases.Clientes;

using DTOs;
using Mappers;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class GetClienteByIdUseCase(IClienteRepository repo)
{
    public async Task<ClienteDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var cliente = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Cliente), id);
        return cliente.ToDto();
    }
}
