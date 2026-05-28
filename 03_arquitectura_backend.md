# Backend Arquitectura Hexagonal — Varilleros Multi-Tenant

## Contexto del proyecto

Sistema SaaS multi-tenant para talleres de reparación de abolladuras sin pintura (PDR / varilleros).  
Cada tenant (empresa) tiene su propia base de datos MySQL/MariaDB aislada.  
La API detecta el tenant por la cabecera HTTP `X-Tenant-Id` (slug del tenant) y resuelve la cadena de conexión en tiempo de ejecución.

---

## Stack tecnológico

| Capa | Tecnología |
|---|---|
| Lenguaje | C# 12 / .NET 9 |
| Framework HTTP | ASP.NET Core 9 Web API |
| ORM | Dapper (micro-ORM, SQL explícito) |
| Base de datos | MySQL 8 / MariaDB 10.6+ |
| Driver MySQL/MariaDB | MySql.Data o MySqlConnector |
| Contenedor DI | Microsoft.Extensions.DependencyInjection (nativo) |
| Logging | Serilog → Console + File |
| Validación | FluentValidation |
| Documentación API | Swashbuckle.AspNetCore (Swagger/OpenAPI) |
| Tests unitarios | xUnit + Moq + FluentAssertions |
| Tests integración | Testcontainers.PostgreSql |

---

## Swagger / OpenAPI Documentation

### Configuración

El proyecto usa **Swashbuckle.AspNetCore** para generar documentación interactiva de la API.

En **Program.cs**:
```csharp
// Registrar Swagger generator
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() 
    { 
        Title = "Varilleros API", 
        Version = "v1",
        Description = "Multi-tenant API para gestión de PDR (varilleros)"
    });
});

// En la pipeline:
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Varilleros API v1");
});
```

### Acceso a la Documentación

- **Development**: Navegar a `https://localhost:{port}/swagger`
- **Production**: Deshabilitado por defecto (requiere configuración explícita en `appsettings.Production.json`)

### Documentación de Endpoints

Todos los controllers documentan cada endpoint con:

```csharp
/// <summary>Obtener cliente por id</summary>
/// <remarks>Requiere cabecera X-Tenant-Id</remarks>
/// <param name="id">Id del cliente</param>
/// <returns>ClienteDto con los datos del cliente</returns>
/// <response code="200">Cliente encontrado</response>
/// <response code="404">Cliente no encontrado</response>
/// <response code="400">Cabecera X-Tenant-Id requerida</response>
[HttpGet("{id:int}")]
[ProduceResponseType(typeof(ClienteDto), 200)]
[ProduceResponseType(404)]
[ProduceResponseType(400)]
public async Task<IActionResult> GetById(int id, CancellationToken ct) =>
    Ok(await getById.ExecuteAsync(id, ct));
```

La documentación OpenAPI refleja automáticamente:
- Todos los endpoints y sus rutas
- Parámetros (ruta, query, body)
- DTOs y sus propiedades (con validaciones)
- Códigos de respuesta (200, 201, 204, 400, 404, 422, 500)

---

## Estructura de solución

```
Varilleros.sln
│
├── src/
│   ├── Varilleros.Domain/              # Entidades, interfaces de repositorio, excepciones de dominio
│   ├── Varilleros.Application/         # Casos de uso (commands/queries), DTOs, interfaces de servicios
│   ├── Varilleros.Infrastructure/      # Implementaciones: repositorios Dapper, resolución de tenant
│   └── Varilleros.Api/                 # Controllers, middleware, DI wiring, Program.cs
│
└── tests/
    ├── Varilleros.Domain.Tests/
    ├── Varilleros.Application.Tests/
    └── Varilleros.Infrastructure.IntegrationTests/
```

### Dependencias entre capas (regla estricta)

```
Api → Application → Domain
Infrastructure → Application → Domain
```

- `Domain` no depende de ningún proyecto propio.  
- `Application` sólo depende de `Domain`.  
- `Infrastructure` implementa interfaces definidas en `Domain` y `Application`.  
- `Api` orquesta el wiring de DI y expone los endpoints.

---

## Bases de datos

### Master DB (`varilleros_master`)

Gestiona tenants y catálogo de módulos. La cadena de conexión se configura en `appsettings.json`.

```json
"ConnectionStrings": {
  "MasterDb": "Server=localhost;Port=3306;Database=varilleros_master;User=...;Password=...;CharSet=utf8mb4;"
}
```

**Tablas:**

- `modules_catalog` — módulos disponibles en el sistema
- `tenants` — empresas registradas con sus credenciales de BBDD (contraseña cifrada)
- `tenant_modules` — qué módulos tiene activos cada tenant

### Tenant DB (una por empresa)

Contiene los datos del negocio. La cadena de conexión se resuelve en runtime a partir del slug en la cabecera HTTP.

**Tablas:**

- `cliente`
- `peritos`
- `articulo`
- `precios`
- `presupuesto`

---

## Capa Domain (`Varilleros.Domain`)

### Entidades

Clases POCO puras, sin dependencias de framework. Propiedades con setters privados, constructor con parámetros obligatorios.

```csharp
// Domain/Entities/Cliente.cs
namespace Varilleros.Domain.Entities;

public sealed class Cliente
{
    public int Id { get; private set; }
    public string NombreCliente { get; private set; }
    public string NifCif { get; private set; }
    public string Direccion { get; private set; }
    public string Poblacion { get; private set; }
    public string Email { get; private set; }
    public string Telefono { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Cliente() { } // Para Dapper

    public static Cliente Create(
        string nombreCliente, string nifCif,
        string direccion, string poblacion,
        string email, string telefono)
    {
        // Guard clauses
        if (string.IsNullOrWhiteSpace(nombreCliente)) throw new DomainException("NombreCliente es obligatorio");
        if (string.IsNullOrWhiteSpace(nifCif))        throw new DomainException("NifCif es obligatorio");
        // … resto de validaciones

        return new Cliente
        {
            NombreCliente = nombreCliente,
            NifCif        = nifCif,
            Direccion     = direccion,
            Poblacion     = poblacion,
            Email         = email,
            Telefono      = telefono,
            CreatedAt     = DateTime.UtcNow,
            UpdatedAt     = DateTime.UtcNow
        };
    }

    public void Update(string nombreCliente, string nifCif,
        string direccion, string poblacion,
        string email, string telefono)
    {
        NombreCliente = nombreCliente;
        NifCif        = nifCif;
        Direccion     = direccion;
        Poblacion     = poblacion;
        Email         = email;
        Telefono      = telefono;
        UpdatedAt     = DateTime.UtcNow;
    }
}
```

**Entidades a generar** (misma estructura):

| Entidad | Tabla | Campos clave |
|---|---|---|
| `Cliente` | `cliente` | id, nombrecliente, nifcif, direccion, poblacion, email, telefono |
| `Perito` | `peritos` | id (bigint identity), nombre, email |
| `Articulo` | `articulo` | id, codigo, descripcion, codigopreciopresupuesto |
| `Precio` | `precios` | numeroabolladuras (PK), aletaleve..montantegrave (18 campos int nullable) |
| `Presupuesto` | `presupuesto` | Ver DDL completo — ~110 columnas agrupadas por panel de carrocería |
| `Tenant` | `tenants` (master) | id, name, slug, db_host, db_port, db_name, db_user, db_password, is_active |
| `ModuleCatalog` | `modules_catalog` (master) | id, code, name, description, is_active |
| `TenantModule` | `tenant_modules` (master) | id, tenant_id, module_id, is_active, granted_at, expires_at |

### Interfaces de repositorio (puertos)

```csharp
// Domain/Repositories/IClienteRepository.cs
namespace Varilleros.Domain.Repositories;

public interface IClienteRepository
{
    Task<Cliente?>          GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Cliente>> GetAllAsync(CancellationToken ct = default);
    Task<int>               CreateAsync(Cliente cliente, CancellationToken ct = default);
    Task                    UpdateAsync(Cliente cliente, CancellationToken ct = default);
    Task                    DeleteAsync(int id, CancellationToken ct = default);
}
```

Crear la misma interfaz para: `IPeritosRepository`, `IArticuloRepository`, `IPreciosRepository`, `IPresupuestoRepository`.

Para la master DB:

```csharp
// Domain/Repositories/Master/ITenantRepository.cs
public interface ITenantRepository
{
    Task<Tenant?> GetBySlugAsync(string slug, CancellationToken ct = default);
    Task<IEnumerable<Tenant>> GetAllAsync(CancellationToken ct = default);
    Task<int>    CreateAsync(Tenant tenant, CancellationToken ct = default);
    Task         UpdateAsync(Tenant tenant, CancellationToken ct = default);
    Task         DeleteAsync(int id, CancellationToken ct = default);
}
```

### Excepciones de dominio

```csharp
// Domain/Exceptions/DomainException.cs
namespace Varilleros.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class NotFoundException : DomainException
{
    public NotFoundException(string entity, object key)
        : base($"{entity} con id '{key}' no encontrado.") { }
}
```

---

## Capa Application (`Varilleros.Application`)

Patrón CQRS sin MediatR (comandos y queries como clases simples con su handler).

### DTOs

```csharp
// Application/DTOs/ClienteDto.cs
public record ClienteDto(
    int    Id,
    string NombreCliente,
    string NifCif,
    string Direccion,
    string Poblacion,
    string Email,
    string Telefono
);

// Application/DTOs/CreateClienteDto.cs
public record CreateClienteDto(
    string NombreCliente,
    string NifCif,
    string Direccion,
    string Poblacion,
    string Email,
    string Telefono
);

// Application/DTOs/UpdateClienteDto.cs  (mismo contenido que Create + int Id)
```

### Casos de uso — ejemplo Cliente

```csharp
// Application/UseCases/Clientes/GetClienteByIdUseCase.cs
public sealed class GetClienteByIdUseCase(IClienteRepository repo)
{
    public async Task<ClienteDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var cliente = await repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Cliente), id);

        return cliente.ToDto();
    }
}
```

**Casos de uso a generar por entidad:**

- `GetAll{Entity}UseCase`
- `Get{Entity}ByIdUseCase`
- `Create{Entity}UseCase`
- `Update{Entity}UseCase`
- `Delete{Entity}UseCase`

### Mappers

```csharp
// Application/Mappers/ClienteMapper.cs
public static class ClienteMapper
{
    public static ClienteDto ToDto(this Cliente c) => new(
        c.Id, c.NombreCliente, c.NifCif,
        c.Direccion, c.Poblacion, c.Email, c.Telefono
    );
}
```

### Validadores (FluentValidation)

```csharp
// Application/Validators/CreateClienteDtoValidator.cs
public class CreateClienteDtoValidator : AbstractValidator<CreateClienteDto>
{
    public CreateClienteDtoValidator()
    {
        RuleFor(x => x.NombreCliente).NotEmpty().MaximumLength(200);
        RuleFor(x => x.NifCif).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Telefono).NotEmpty();
        RuleFor(x => x.Direccion).NotEmpty();
        RuleFor(x => x.Poblacion).NotEmpty();
    }
}
```

---

## Capa Infrastructure (`Varilleros.Infrastructure`)

### Resolución de tenant (pieza clave del multi-tenancy)

```csharp
// Infrastructure/MultiTenancy/TenantContext.cs
public sealed class TenantContext
{
    public string Slug      { get; init; } = default!;
    public string DbConnStr { get; init; } = default!;
}

// Infrastructure/MultiTenancy/ITenantResolver.cs
public interface ITenantResolver
{
    Task<TenantContext> ResolveAsync(string slug, CancellationToken ct = default);
}

// Infrastructure/MultiTenancy/TenantResolver.cs
// Consulta la master DB, cachea en IMemoryCache (TTL configurable)
public sealed class TenantResolver(
    IDbConnectionFactory masterFactory,
    IMemoryCache cache,
    IOptions<TenantCacheOptions> options) : ITenantResolver
{
    public async Task<TenantContext> ResolveAsync(string slug, CancellationToken ct = default)
    {
        var cacheKey = $"tenant:{slug}";
        if (cache.TryGetValue(cacheKey, out TenantContext? cached))
            return cached!;

        using var conn = masterFactory.CreateConnection();
        var tenant = await conn.QuerySingleOrDefaultAsync<TenantRow>(
            "SELECT * FROM tenants WHERE slug = @slug AND is_active = TRUE",
            new { slug });

        if (tenant is null)
            throw new TenantNotFoundException(slug);

        var ctx = new TenantContext
        {
            Slug     = tenant.Slug,
            DbConnStr = BuildConnectionString(tenant)
        };

        cache.Set(cacheKey, ctx, options.Value.Ttl);
        return ctx;
    }

    private static string BuildConnectionString(TenantRow t) =>
        $"Server={t.DbHost};Port={t.DbPort};Database={t.DbName};User={t.DbUser};Password={t.DbPassword};CharSet=utf8mb4;";
}
```

### Fábrica de conexiones

```csharp
// Infrastructure/Data/IDbConnectionFactory.cs
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

// Infrastructure/Data/MySqlConnectionFactory.cs
// Paquete NuGet: MySqlConnector  (recomendado sobre MySql.Data por ser async-nativo)
public sealed class MySqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => new MySqlConnection(connectionString);
}

// Infrastructure/Data/TenantDbConnectionFactory.cs
// Resuelve la conexión del tenant en cada request
public sealed class TenantDbConnectionFactory(
    IHttpContextAccessor httpCtx,
    ITenantResolver resolver) : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var slug = httpCtx.HttpContext!.Request.Headers["X-Tenant-Id"].FirstOrDefault()
            ?? throw new InvalidOperationException("Cabecera X-Tenant-Id no presente");

        // Nota: ResolveAsync es async pero CreateConnection es sync.
        // Registrar TenantContext como Scoped en DI y resolverlo en el middleware.
        // Ver TenantMiddleware más abajo.
        var ctx = httpCtx.HttpContext!.RequestServices.GetRequiredService<TenantContext>();
        return new MySqlConnection(ctx.DbConnStr);
    }
}
```

### Middleware de tenant

```csharp
// Api/Middleware/TenantMiddleware.cs
public sealed class TenantMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext ctx, ITenantResolver resolver)
    {
        var slug = ctx.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(slug))
        {
            ctx.Response.StatusCode = 400;
            await ctx.Response.WriteAsync("Cabecera X-Tenant-Id requerida");
            return;
        }

        var tenantCtx = await resolver.ResolveAsync(slug, ctx.RequestAborted);
        ctx.RequestServices.GetRequiredService<TenantContextHolder>().Set(tenantCtx);
        await next(ctx);
    }
}
```

### Repositorios (adaptadores Dapper)

```csharp
// Infrastructure/Repositories/ClienteRepository.cs
public sealed class ClienteRepository(IDbConnectionFactory factory) : IClienteRepository
{
    public async Task<Cliente?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Cliente>(
            "SELECT * FROM cliente WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        return await conn.QueryAsync<Cliente>("SELECT * FROM cliente ORDER BY id");
    }

    public async Task<int> CreateAsync(Cliente cliente, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        // MySQL no tiene RETURNING; se usa ExecuteAsync + LastInsertId
        await conn.ExecuteAsync("""
            INSERT INTO cliente (nombrecliente, nifcif, direccion, poblacion, email, telefono, created_at, updated_at)
            VALUES (@NombreCliente, @NifCif, @Direccion, @Poblacion, @Email, @Telefono, @CreatedAt, @UpdatedAt)
            """, cliente);
        return await conn.ExecuteScalarAsync<int>("SELECT LAST_INSERT_ID()");
    }

    public async Task UpdateAsync(Cliente cliente, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE cliente SET
                nombrecliente = @NombreCliente,
                nifcif        = @NifCif,
                direccion     = @Direccion,
                poblacion     = @Poblacion,
                email         = @Email,
                telefono      = @Telefono,
                updated_at    = @UpdatedAt
            WHERE id = @Id
            """, cliente);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        using var conn = factory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM cliente WHERE id = @id", new { id });
    }
}
```

**Repositorios a generar** con la misma estructura:  
`PeritosRepository`, `ArticuloRepository`, `PreciosRepository`, `PresupuestoRepository`  

Para master DB: `TenantRepository`, `ModuleCatalogRepository`, `TenantModuleRepository`

---

## Capa API (`Varilleros.Api`)

### Controller ejemplo

```csharp
// Api/Controllers/ClientesController.cs
[ApiController]
[Route("api/[controller]")]
public sealed class ClientesController(
    GetAllClientesUseCase    getAll,
    GetClienteByIdUseCase    getById,
    CreateClienteUseCase     create,
    UpdateClienteUseCase     update,
    DeleteClienteUseCase     delete) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await getAll.ExecuteAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) =>
        Ok(await getById.ExecuteAsync(id, ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClienteDto dto, CancellationToken ct)
    {
        var id = await create.ExecuteAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClienteDto dto, CancellationToken ct)
    {
        await update.ExecuteAsync(id, dto, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await delete.ExecuteAsync(id, ct);
        return NoContent();
    }
}
```

**Controllers a generar:** `ClientesController`, `PeritosController`, `ArticulosController`, `PreciosController`, `PresupuestosController`  
Para admin/master: `TenantsController`, `ModulesCatalogController`, `TenantModulesController`

### Gestión global de errores

```csharp
// Api/Middleware/ExceptionMiddleware.cs
public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try { await next(ctx); }
        catch (NotFoundException ex)
        {
            ctx.Response.StatusCode = 404;
            await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (DomainException ex)
        {
            ctx.Response.StatusCode = 422;
            await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (ValidationException ex)
        {
            ctx.Response.StatusCode = 400;
            await ctx.Response.WriteAsJsonAsync(new
            {
                error  = "Validación fallida",
                errors = ex.Errors.Select(e => e.ErrorMessage)
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error no controlado");
            ctx.Response.StatusCode = 500;
            await ctx.Response.WriteAsJsonAsync(new { error = "Error interno del servidor" });
        }
    }
}
```

### Program.cs — wiring de DI

```csharp
var builder = WebApplication.CreateBuilder(args);

// --- Logging ---
builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration));

// --- Master DB factory ---
builder.Services.AddSingleton<IDbConnectionFactory>(
    new MySqlConnectionFactory(builder.Configuration.GetConnectionString("MasterDb")!));

// --- Multi-tenancy ---
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TenantContext>();          // placeholder scoped
builder.Services.AddScoped<TenantContextHolder>();
builder.Services.AddSingleton<ITenantResolver, TenantResolver>();

// --- Tenant DB factory (scoped, se resuelve tras el middleware) ---
builder.Services.AddScoped<IDbConnectionFactory, TenantDbConnectionFactory>();

// --- Repositorios tenant ---
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPeritosRepository, PeritosRepository>();
builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>();
builder.Services.AddScoped<IPreciosRepository, PreciosRepository>();
builder.Services.AddScoped<IPresupuestoRepository, PresupuestoRepository>();

// --- Repositorios master ---
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IModuleCatalogRepository, ModuleCatalogRepository>();
builder.Services.AddScoped<ITenantModuleRepository, TenantModuleRepository>();

// --- Use cases ---
builder.Services.AddScoped<GetAllClientesUseCase>();
builder.Services.AddScoped<GetClienteByIdUseCase>();
builder.Services.AddScoped<CreateClienteUseCase>();
builder.Services.AddScoped<UpdateClienteUseCase>();
builder.Services.AddScoped<DeleteClienteUseCase>();
// … repetir por entidad

// --- Validadores ---
builder.Services.AddValidatorsFromAssemblyContaining<CreateClienteDtoValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TenantMiddleware>();   // debe ir ANTES de los controllers

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
```

---

## Rutas de API

### Endpoints tenant (requieren cabecera `X-Tenant-Id: {slug}`)

| Método | Ruta | Descripción |
|---|---|---|
| GET | /api/clientes | Listar todos |
| GET | /api/clientes/{id} | Obtener por id |
| POST | /api/clientes | Crear |
| PUT | /api/clientes/{id} | Actualizar |
| DELETE | /api/clientes/{id} | Eliminar |
| GET | /api/peritos | Listar todos |
| GET | /api/peritos/{id} | Obtener por id |
| POST | /api/peritos | Crear |
| PUT | /api/peritos/{id} | Actualizar |
| DELETE | /api/peritos/{id} | Eliminar |
| GET | /api/articulos | Listar todos |
| GET | /api/articulos/{id} | Obtener por id |
| POST | /api/articulos | Crear |
| PUT | /api/articulos/{id} | Actualizar |
| DELETE | /api/articulos/{id} | Eliminar |
| GET | /api/precios | Listar todos |
| GET | /api/precios/{numeroabolladuras} | Obtener por nº abolladuras |
| POST | /api/precios | Crear/upsert |
| PUT | /api/precios/{numeroabolladuras} | Actualizar |
| DELETE | /api/precios/{numeroabolladuras} | Eliminar |
| GET | /api/presupuestos | Listar (paginado) |
| GET | /api/presupuestos/{id} | Obtener por id |
| POST | /api/presupuestos | Crear |
| PUT | /api/presupuestos/{id} | Actualizar |
| DELETE | /api/presupuestos/{id} | Eliminar |

### Endpoints master (sin cabecera de tenant)

| Método | Ruta | Descripción |
|---|---|---|
| GET | /api/admin/tenants | Listar tenants |
| GET | /api/admin/tenants/{id} | Obtener tenant |
| POST | /api/admin/tenants | Crear tenant |
| PUT | /api/admin/tenants/{id} | Actualizar tenant |
| DELETE | /api/admin/tenants/{id} | Eliminar tenant |
| GET | /api/admin/modules | Listar módulos |
| POST | /api/admin/modules | Crear módulo |
| GET | /api/admin/tenants/{id}/modules | Módulos de un tenant |
| POST | /api/admin/tenants/{id}/modules/{moduleId} | Asignar módulo |
| DELETE | /api/admin/tenants/{id}/modules/{moduleId} | Revocar módulo |

---

## Notas especiales

### Tabla `presupuesto`

Tiene ~110 columnas agrupadas por panel de carrocería. Cada panel sigue el patrón:

```
{PANEL}leve, {PANEL}totaldanyoleve,
{PANEL}medio, {PANEL}totaldanyomedio,
{PANEL}grave, {PANEL}totaldanyograve,
{PANEL}pintura (bool), {PANEL}aluminio (bool),
{PANEL}total
```

Paneles: `ADI`, `ADD`, `ATI`, `ATD`, `PDI`, `PDD`, `PTD`, `PTI`, `CAPO`, `TECHO`, `PORTON`, `MI`, `MD`

El DTO de presupuesto puede representar cada panel como un objeto anidado `PanelDto` en el frontend, pero en BBDD están desnormalizadas en columnas planas.

### Tabla `precios`

La clave primaria `numeroabolladuras` es un entero que representa la fila de tarifa. Los endpoints usan ese valor como `{id}` en la ruta.

### Cache de tenants

Configurable en `appsettings.json`:

```json
"TenantCache": {
  "TtlSeconds": 300
}
```

### Seguridad de credenciales de tenant

Las contraseñas en `tenants.db_password` deben almacenarse cifradas. Implementar `IEncryptionService` en Infrastructure usando AES-256 o delegar a un vault (HashiCorp Vault / Azure Key Vault). No implementar en este prompt — marcar como `TODO`.

---

## Checklist de generación

- [ ] Entidades Domain con factory method `Create` y método `Update`  
- [ ] Excepciones `DomainException` y `NotFoundException`  
- [ ] Interfaces de repositorio en Domain  
- [ ] DTOs en Application (Create, Update, Response)  
- [ ] Validadores FluentValidation para Create y Update  
- [ ] Mappers estáticos Entity ↔ DTO  
- [ ] 5 casos de uso por entidad (GetAll, GetById, Create, Update, Delete)  
- [ ] Repositorios Dapper en Infrastructure  
- [ ] `TenantResolver` + `TenantMiddleware`  
- [ ] `MySqlConnectionFactory` y `TenantDbConnectionFactory`  
- [ ] `ExceptionMiddleware`  
- [ ] Controllers (un controller por entidad, rutas RESTful)  
- [ ] `Program.cs` completo con todo el wiring  
- [ ] `appsettings.json` con secciones: `ConnectionStrings:MasterDb`, `TenantCache`, `Serilog`
- [ ] Swagger/OpenAPI configurado con `Swashbuckle.AspNetCore`  
