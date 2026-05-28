namespace Varilleros.src.Application.UseCases.Clientes;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Repositories;

public sealed class CreateClienteUseCase(IClienteRepository repo)
{
    public async Task<int> ExecuteAsync(CreateClienteDto dto, CancellationToken ct = default)
    {
        var cliente = Cliente.Create(
            dto.NombreCliente, dto.NifCif, dto.Direccion,
            dto.Poblacion, dto.Email, dto.Telefono);
        return await repo.CreateAsync(cliente, ct);
    }
}
