# Contexto del Proyecto — Varilleros Multi-Tenant API

> **Uso:** Pega este documento al inicio de cualquier conversación con una IA para que entienda el proyecto al instante.

---

## 1. ¿Qué es este proyecto?

SaaS multi-tenant para talleres de **reparación de abolladuras (PDR / varilleros)**.  
Cada empresa cliente (tenant) tiene su propia base de datos MySQL/MariaDB aislada.  
La API detecta el tenant por la cabecera HTTP `X-Tenant-Id: {slug}` en cada request.

---

## 2. Stack tecnológico

| Capa | Tecnología |
|---|---|
| Lenguaje | C# 12 / .NET 10 |
| Framework HTTP | ASP.NET Core 10 Web API |
| ORM | Dapper (micro-ORM, SQL explícito — sin Entity Framework) |
| Base de datos | MySQL 8 / MariaDB 10.6+ |
| Driver MySQL | MySqlConnector 2.x |
| Contenedor DI | Microsoft.Extensions.DependencyInjection (nativo) |
| Logging | Serilog → Console + File rolling diario |
| Validación | FluentValidation 11 |
| Documentación API | Swashbuckle.AspNetCore (Swagger) en `/swagger` |
| Caché | IMemoryCache (para tenant resolver) |

---

## 3. Arquitectura

**Hexagonal (Ports & Adapters)** + **CQRS sin MediatR** (comandos y queries como clases simples).

### Regla de dependencias (estricta)
```
Api → Application → Domain
Infrastructure → Application → Domain
```
- `Domain` no depende de nada propio.
- `Application` solo depende de `Domain`.
- `Infrastructure` implementa interfaces de `Domain`.
- `Api` (Program.cs) orquesta el wiring de DI.

### Estructura de carpetas (un solo .csproj, separación lógica)
```
varilleros/
├── Program.cs
├── varilleros.csproj
├── appsettings.json
└── src/
    ├── Domain/
    │   ├── Entities/           ← 8 entidades POCO (setters privados)
    │   ├── Repositories/       ← Interfaces de repositorio (puertos)
    │   │   └── Master/         ← Interfaces para la master DB
    │   └── Exceptions/         ← DomainException, NotFoundException, TenantNotFoundException
    │
    ├── Application/
    │   ├── DTOs/               ← Records: {Entity}Dto, Create{Entity}Dto, Update{Entity}Dto
    │   ├── Mappers/            ← Extension methods .ToDto() por entidad
    │   ├── Validators/         ← FluentValidation: uno por DTO
    │   └── UseCases/
    │       ├── Clientes/       ← GetAll, GetById, Create, Update, Delete
    │       ├── Peritos/
    │       ├── Articulos/
    │       ├── Precios/
    │       ├── Presupuestos/
    │       ├── Tenants/        ← Admin (master DB)
    │       ├── Modules/        ← Admin (master DB)
    │       └── TenantModules/  ← Admin (master DB)
    │
    ├── Infrastructure/
    │   ├── Data/
    │   │   ├── IDbConnectionFactory.cs
    │   │   ├── MySqlConnectionFactory.cs   ← Para master DB (Singleton)
    │   │   └── TenantDbConnectionFactory.cs ← Para tenant DB (Scoped)
    │   ├── MultiTenancy/
    │   │   ├── TenantContext.cs            ← { Slug, DbConnStr }
    │   │   ├── TenantContextHolder.cs      ← Holder scoped por request
    │   │   ├── ITenantResolver.cs
    │   │   └── TenantResolver.cs           ← Consulta master DB + caché TTL
    │   └── Repositories/
    │       ├── ClienteRepository.cs        ← Dapper
    │       ├── PeritosRepository.cs
    │       ├── ArticuloRepository.cs
    │       ├── PreciosRepository.cs
    │       ├── PresupuestoRepository.cs
    │       └── Master/
    │           ├── TenantRepository.cs
    │           ├── ModuleCatalogRepository.cs
    │           └── TenantModuleRepository.cs
    │
    └── Api/
        ├── Controllers/
        │   ├── ClientesController.cs
        │   ├── PeritosController.cs
        │   ├── ArticulosController.cs
        │   ├── PreciosController.cs
        │   ├── PresupuestosController.cs
        │   └── Admin/
        │       ├── TenantsController.cs
        │       ├── ModulesCatalogController.cs
        │       └── TenantModulesController.cs
        └── Middleware/
            ├── TenantMiddleware.cs     ← Resuelve X-Tenant-Id antes de los controllers
            └── ExceptionMiddleware.cs  ← Convierte excepciones en respuestas HTTP
```

---

## 4. Multi-tenancy — cómo funciona

1. El cliente envía `X-Tenant-Id: acme` en la cabecera HTTP.
2. `TenantMiddleware` llama a `TenantResolver.ResolveAsync("acme")`.
3. `TenantResolver` busca el tenant en la **master DB** (`varilleros_master.tenants`).
4. La cadena de conexión se construye y se almacena en `TenantContextHolder` (Scoped).
5. `TenantDbConnectionFactory` lee esa cadena para abrir conexiones al hacer queries.
6. El resultado se cachea en `IMemoryCache` (TTL configurable, por defecto 5 min).

**Endpoints admin** (`/api/admin/...`) **no pasan por TenantMiddleware** — trabajan directamente contra la master DB.

---

## 5. Bases de datos

### Master DB: `varilleros_master`

```sql
-- Módulos disponibles en el sistema
modules_catalog (id, code, name, description, is_active, created_at, updated_at)
  Módulos semilla: CLIENTES, PERITOS, PRECIOS, PRESUPUESTOS, ARTICULOS

-- Empresas registradas
tenants (id, name, slug, db_host, db_port, db_name, db_user, db_password, is_active, created_at, updated_at)
  Nota: db_password debe almacenarse cifrada (TODO: AES-256 / vault)
  slug es el valor que el cliente pone en X-Tenant-Id

-- Módulos activos por empresa
tenant_modules (id, tenant_id, module_id, is_active, granted_at, expires_at)
  expires_at NULL = licencia indefinida
```

### Tenant DB: `varilleros_{slug}` (una por empresa)

```sql
-- Clientes del taller
cliente (id INT AI, nombrecliente, nifcif, direccion, poblacion, email, telefono, created_at, updated_at)

-- Peritos de seguros
peritos (id BIGINT AI, nombre, email, created_at, updated_at)

-- Catálogo de artículos/repuestos
articulo (id INT AI, codigo, descripcion, codigopreciopresupuesto, created_at, updated_at)

-- Tarifa de precios por número de abolladuras y zona
precios (
  numeroabolladuras INT PK,  ← clave primaria, no auto-increment
  aletaleve, aletamedio, aletagrave,
  puertaleve, puertamedio, puertagrave,
  techoleve, techomedio, techograve,
  capoleve, capomedio, capograve,
  portonleve, portonmedio, portongrave,
  montanteleve, montantemedio, montantegrave,
  updated_at
)

-- Presupuestos de reparación (tabla desnormalizada ~110 columnas)
presupuesto (
  id INT AI PK,
  reparador, marca, modelo, matricula, preciototal,
  -- 13 paneles × 9 columnas cada uno:
  -- Paneles: ADI, ADD, ATI, ATD, PDI, PDD, PTD, PTI, CAPO, TECHO, PORTON, MI, MD
  -- Por panel: {PANEL}leve, {PANEL}totaldanyoleve,
  --            {PANEL}medio, {PANEL}totaldanyomedio,
  --            {PANEL}grave, {PANEL}totaldanyograve,
  --            {PANEL}pintura (bool), {PANEL}aluminio (bool), {PANEL}total
  fechaCreacion BIGINT,      ← timestamp UNIX heredado
  descuento SMALLINT,
  observaciones TEXT,
  desmontajes SMALLINT,
  estado SMALLINT,           ← 1=Borrador 2=Enviado 3=Aceptado 4=Rechazado
  -- Snapshot del cliente en el momento del presupuesto:
  nombrecliente, direccion, poblacion, nifcif, email, telefono,
  aseguradora, idPerito BIGINT,
  created_at, updated_at
)
```

---

## 6. Endpoints de la API

### Tenant (requieren cabecera `X-Tenant-Id: {slug}`)

| Método | Ruta | Descripción |
|---|---|---|
| GET | /api/clientes | Listar clientes |
| GET | /api/clientes/{id} | Obtener cliente |
| POST | /api/clientes | Crear cliente |
| PUT | /api/clientes/{id} | Actualizar cliente |
| DELETE | /api/clientes/{id} | Eliminar cliente |
| GET | /api/peritos | Listar peritos |
| GET | /api/peritos/{id} | Obtener perito |
| POST | /api/peritos | Crear perito |
| PUT | /api/peritos/{id} | Actualizar perito |
| DELETE | /api/peritos/{id} | Eliminar perito |
| GET | /api/articulos | Listar artículos |
| GET | /api/articulos/{id} | Obtener artículo |
| POST | /api/articulos | Crear artículo |
| PUT | /api/articulos/{id} | Actualizar artículo |
| DELETE | /api/articulos/{id} | Eliminar artículo |
| GET | /api/precios | Listar tabla de precios |
| GET | /api/precios/{numeroabolladuras} | Obtener fila de precio |
| POST | /api/precios | Crear fila de precio |
| PUT | /api/precios/{numeroabolladuras} | Actualizar precio |
| DELETE | /api/precios/{numeroabolladuras} | Eliminar precio |
| GET | /api/presupuestos | Listar presupuestos |
| GET | /api/presupuestos/{id} | Obtener presupuesto |
| POST | /api/presupuestos | Crear presupuesto |
| PUT | /api/presupuestos/{id} | Actualizar presupuesto |
| DELETE | /api/presupuestos/{id} | Eliminar presupuesto |

### Admin / Master (sin cabecera de tenant)

| Método | Ruta | Descripción |
|---|---|---|
| GET | /api/admin/tenants | Listar tenants |
| GET | /api/admin/tenants/{id} | Obtener tenant |
| POST | /api/admin/tenants | Crear tenant |
| PUT | /api/admin/tenants/{id} | Actualizar tenant |
| DELETE | /api/admin/tenants/{id} | Eliminar tenant |
| GET | /api/admin/modules | Listar módulos del catálogo |
| POST | /api/admin/modules | Crear módulo |
| GET | /api/admin/tenants/{id}/modules | Módulos activos de un tenant |
| POST | /api/admin/tenants/{id}/modules/{moduleId} | Asignar módulo a tenant |
| DELETE | /api/admin/tenants/{id}/modules/{moduleId} | Revocar módulo de tenant |

---

## 7. DTOs (contratos de la API)

```csharp
// Cliente
record ClienteDto(int Id, string NombreCliente, string NifCif, string Direccion, string Poblacion, string Email, string Telefono);
record CreateClienteDto(string NombreCliente, string NifCif, string Direccion, string Poblacion, string Email, string Telefono);
record UpdateClienteDto(int Id, string NombreCliente, string NifCif, string Direccion, string Poblacion, string Email, string Telefono);

// Perito
record PeritoDto(long Id, string Nombre, string Email);
record CreatePeritoDto(string Nombre, string Email);
record UpdatePeritoDto(long Id, string Nombre, string Email);

// Articulo
record ArticuloDto(int Id, string Codigo, string Descripcion, string CodigoPrecioPresupuesto);
record CreateArticuloDto(string Codigo, string Descripcion, string CodigoPrecioPresupuesto);
record UpdateArticuloDto(int Id, string Codigo, string Descripcion, string CodigoPrecioPresupuesto);

// Precio — PK es numeroabolladuras (no auto-increment)
record PrecioDto(int NumeroabolladuraS, int? AletaLeve, int? AletaMedio, int? AletaGrave, ...);
record CreatePrecioDto(int NumeroabolladuraS);
record UpdatePrecioDto(int NumeroabolladuraS, int? AletaLeve, int? AletaMedio, ...);

// Presupuesto
record PresupuestoDto(int Id, int ClienteId, int PeritoId, DateTime FechaPresupuesto, string Descripcion, decimal TotalPresupuesto, string Estado);
record CreatePresupuestoDto(int ClienteId, int PeritoId, string Descripcion, decimal TotalPresupuesto);
record UpdatePresupuestoDto(int Id, string Descripcion, decimal TotalPresupuesto, string Estado);

// Tenant (Admin)
record TenantDto(int Id, string Name, string Slug, bool IsActive);
record CreateTenantDto(string Name, string Slug, string DbHost, int DbPort, string DbName, string DbUser, string DbPassword);
record UpdateTenantDto(int Id, string Name, string DbHost, int DbPort, string DbName, string DbUser, string DbPassword);

// ModuleCatalog (Admin)
record ModuleCatalogDto(int Id, string Code, string Name, string Description, bool IsActive);
record CreateModuleCatalogDto(string Code, string Name, string Description);

// TenantModule (Admin)
record TenantModuleDto(int Id, int TenantId, int ModuleId, bool IsActive, DateTime? GrantedAt, DateTime? ExpiresAt);
record CreateTenantModuleDto(int TenantId, int ModuleId, DateTime? ExpiresAt);
```

---

## 8. Convenciones de código

### Entidades Domain
- Clases `sealed`, propiedades con `private set`.
- Constructor privado vacío (para Dapper).
- Factory method estático `Create(...)` con guard clauses → lanza `DomainException`.
- Método `Update(...)` que actualiza campos y pone `UpdatedAt = DateTime.UtcNow`.

```csharp
public sealed class Cliente
{
    public int Id { get; private set; }
    public string NombreCliente { get; private set; } = null!;
    // ... más propiedades

    private Cliente() { }  // Para Dapper

    public static Cliente Create(string nombre, ...) { /* guard clauses */ }
    public void Update(string nombre, ...) { /* actualiza + UpdatedAt */ }
}
```

### Use Cases
- Clases `sealed`, un método `ExecuteAsync(...)`.
- Se inyecta solo el repositorio que necesita.
- Si el registro no existe → `throw new NotFoundException(nameof(Entity), id)`.

```csharp
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

### Repositorios (Dapper)
- Cada método abre y cierra su propia conexión (`using var conn = factory.CreateConnection()`).
- MySQL: no tiene `RETURNING` → usar `SELECT LAST_INSERT_ID()` para obtener el id insertado.
- SQL en raw strings `""" ... """`.

### Respuestas HTTP (ExceptionMiddleware)
| Excepción | HTTP |
|---|---|
| `NotFoundException` | 404 |
| `DomainException` | 422 |
| `ValidationException` (FluentValidation) | 400 |
| `TenantNotFoundException` | 404 (interceptada en TenantMiddleware) |
| Cualquier otra | 500 |

---

## 9. Configuración (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "MasterDb": "Server=localhost;Port=3306;Database=varilleros_master;User=root;Password=;CharSet=utf8mb4;"
  },
  "TenantCache": {
    "TtlSeconds": 300
  },
  "Serilog": {
    "MinimumLevel": { "Default": "Information" },
    "WriteTo": [
      { "Name": "Console" },
      { "Name": "File", "Args": { "path": "logs/varilleros-.txt", "rollingInterval": "Day" } }
    ]
  }
}
```

---

## 10. Paquetes NuGet instalados

```xml
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
<PackageReference Include="Dapper" Version="2.1.x" />
<PackageReference Include="MySqlConnector" Version="2.3.x" />
<PackageReference Include="FluentValidation" Version="11.10.0" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.10.0" />
<PackageReference Include="Serilog.AspNetCore" Version="8.0.2" />
<PackageReference Include="Serilog.Sinks.File" Version="5.0.0" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.x" />
```

---

## 11. Ruta del proyecto

```
C:\Users\alejandro\Desktop\varilleros_tenant\varilleros_api\varilleros\varilleros\
```

Scripts SQL de referencia:
- `C:\Users\alejandro\Desktop\varilleros_tenant\01_master_db_scripts.sql`
- `C:\Users\alejandro\Desktop\varilleros_tenant\02_tenant_db_scripts.sql`

---

## 12. TODOs / Pendientes

- [ ] **Cifrado de contraseñas** en `tenants.db_password` → implementar `IEncryptionService` con AES-256 o vault
- [ ] **Autenticación/Autorización** en endpoints (JWT o API Key para proteger admin)
- [ ] **Paginación** en `GET /api/presupuestos`
- [ ] **Entidad Presupuesto** completa (actualmente simplificada; la tabla real tiene ~110 columnas por paneles de carrocería)
- [ ] **Tests unitarios** (xUnit + Moq + FluentAssertions)
- [ ] **Tests de integración** (Testcontainers para levantar MySQL en CI)
- [ ] **Middleware de tenant excluido** para rutas `/api/admin/...` (actualmente el middleware aplica a todas las rutas)
