namespace Varilleros.src.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class NotFoundException : DomainException
{
    public NotFoundException(string entity, object key)
        : base($"{entity} con id '{key}' no encontrado.") { }
}

public class TenantNotFoundException : DomainException
{
    public TenantNotFoundException(string slug)
        : base($"Tenant '{slug}' no encontrado o inactivo.") { }
}
