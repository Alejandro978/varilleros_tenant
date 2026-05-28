namespace Varilleros.src.Application.Mappers;

using DTOs;
using Varilleros.src.Domain.Entities;

public static class PeritoMapper
{
    public static PeritoDto ToDto(this Perito p) => new(p.Id, p.Nombre, p.Email);
}
