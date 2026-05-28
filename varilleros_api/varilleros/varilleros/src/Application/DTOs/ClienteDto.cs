namespace Varilleros.src.Application.DTOs;

public record ClienteDto(
    int Id,
    string NombreCliente,
    string NifCif,
    string Direccion,
    string Poblacion,
    string Email,
    string Telefono
);

public record CreateClienteDto(
    string NombreCliente,
    string NifCif,
    string Direccion,
    string Poblacion,
    string Email,
    string Telefono
);

public record UpdateClienteDto(
    int Id,
    string NombreCliente,
    string NifCif,
    string Direccion,
    string Poblacion,
    string Email,
    string Telefono
);
