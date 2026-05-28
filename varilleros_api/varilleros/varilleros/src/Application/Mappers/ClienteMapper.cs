namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class ClienteMapper
{
    public static ClienteDto ToDto(this Cliente c) => new(
        c.Id, c.NombreCliente, c.NifCif, c.Direccion, c.Poblacion, c.Email, c.Telefono);
}
