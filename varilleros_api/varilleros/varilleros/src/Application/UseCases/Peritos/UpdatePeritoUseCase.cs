namespace Varilleros.src.Application.UseCases.Peritos;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class UpdatePeritoUseCase(IPeritosRepository repo)
{
    public async Task ExecuteAsync(long id, UpdatePeritoDto dto, CancellationToken ct = default)
    {
        var perito = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Perito), id);
        perito.Update(dto.Nombre, dto.Email);
        await repo.UpdateAsync(perito, ct);
    }
}
