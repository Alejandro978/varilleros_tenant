namespace Varilleros.src.Application.DTOs;

public record PeritoDto(long Id, string Nombre, string Email);

public record CreatePeritoDto(string Nombre, string Email);

public record UpdatePeritoDto(long Id, string Nombre, string Email);
