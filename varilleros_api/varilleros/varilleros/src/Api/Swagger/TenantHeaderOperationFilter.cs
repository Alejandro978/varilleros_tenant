namespace Varilleros.src.Api.Swagger;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
/// Añade el header X-Tenant-Id como parámetro requerido en todos los endpoints
/// que NO pertenecen a las rutas de administración (/api/admin/...).
/// </summary>
public sealed class TenantHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var path = context.ApiDescription.RelativePath ?? string.Empty;

        // Las rutas de admin usan la master DB y no necesitan tenant
        if (path.StartsWith("api/admin", StringComparison.OrdinalIgnoreCase))
            return;

        operation.Parameters ??= [];
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Tenant-Id",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema { Type = "string", Example = new Microsoft.OpenApi.Any.OpenApiString("mi-tenant") },
            Description = "Slug del tenant al que pertenece la petición (ej: acme, varilleros-bcn)"
        });
    }
}
