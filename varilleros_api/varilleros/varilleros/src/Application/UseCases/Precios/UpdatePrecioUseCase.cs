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
            dto.AletaLeve,  dto.AletaMedio,  dto.AletaGrave,
            dto.PuertaLeve, dto.PuertaMedio, dto.PuertaGrave,
            dto.TechoLeve,  dto.TechoMedio,  dto.TechoGrave,
            dto.CapoLeve,   dto.CapoMedio,   dto.CapoGrave,
            dto.PortonLeve, dto.PortonMedio, dto.PortonGrave,
            dto.MontanteLeve, dto.MontanteMedio, dto.MontanteGrave);
        await repo.UpdateAsync(precio, ct);
    }
}
