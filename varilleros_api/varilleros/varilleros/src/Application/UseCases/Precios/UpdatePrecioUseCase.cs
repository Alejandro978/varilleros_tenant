namespace Varilleros.src.Application.UseCases.Precios;

using DTOs;
using Varilleros.src.Domain.Entities;
using Varilleros.src.Domain.Exceptions;
using Varilleros.src.Domain.Repositories;

public sealed class UpdatePrecioUseCase(IPreciosRepository repo)
{
    public async Task ExecuteAsync(int numeroabolladuras, UpdatePrecioDto dto, CancellationToken ct = default)
    {
        var precio = await repo.GetByIdAsync(numeroabolladuras, ct)
            ?? throw new NotFoundException(nameof(Precio), numeroabolladuras);
        precio.Update(
            dto.AletaLeve, dto.AletaMedio, dto.AletaGrave,
            dto.AcristalaminaLeve, dto.AcristalaminaMedio, dto.AcristalaminaGrave,
            dto.AletatrasLeve, dto.AletatrasMedio, dto.AletatrasGrave,
            dto.AsientoLeve, dto.AsientoMedio, dto.AsientoGrave,
            dto.AsientoDtraLeve, dto.AsientoDtraMedio, dto.AsientoDtraGrave,
            dto.MaletaLeve, dto.MaletaMedio, dto.MaletaGrave);
        await repo.UpdateAsync(precio, ct);
    }
}
