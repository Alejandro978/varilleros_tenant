namespace Varilleros.src.Application.UseCases.Clientes;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class UpdateClienteUseCase(IClienteRepository repo)
{
    public async Task ExecuteAsync(int id, UpdateClienteDto dto, CancellationToken ct = default)
    {
        var cliente = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Cliente), id);
        cliente.Update(dto.NombreCliente, dto.NifCif, dto.Direccion,
            dto.Poblacion, dto.Email, dto.Telefono);
        await repo.UpdateAsync(cliente, ct);
    }
}
