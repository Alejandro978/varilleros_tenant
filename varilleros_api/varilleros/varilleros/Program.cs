using System.Text;
using Dapper;
using FluentValidation;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Varilleros.src.Application.UseCases.Articulos;
using Varilleros.src.Application.UseCases.Auth;
using Varilleros.src.Application.UseCases.Clientes;
using Varilleros.src.Application.UseCases.Modules;
using Varilleros.src.Application.UseCases.Peritos;
using Varilleros.src.Application.UseCases.Precios;
using Varilleros.src.Application.UseCases.Presupuestos;
using Varilleros.src.Application.UseCases.Tenants;
using Varilleros.src.Application.UseCases.TenantModules;
using Varilleros.src.Application.Validators;
using Varilleros.src.Domain.Repositories;
using Varilleros.src.Domain.Repositories.Master;
using Varilleros.src.Infrastructure.Data;
using Varilleros.src.Infrastructure.MultiTenancy;
using Varilleros.src.Infrastructure.Repositories;
using Varilleros.src.Infrastructure.Repositories.Master;
using Varilleros.src.Api.Middleware;
using Varilleros.src.Api.Swagger;

// Mapeo automático snake_case → PascalCase para Dapper
DefaultTypeMap.MatchNamesWithUnderscores = true;

var builder = WebApplication.CreateBuilder(args);

// --- Logging ---
builder.Host.UseSerilog((ctx, cfg) =>
    cfg
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("logs/varilleros-.txt", rollingInterval: RollingInterval.Day)
        .ReadFrom.Configuration(ctx.Configuration));

// --- Controllers + Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Varilleros API",
        Version = "v1",
        Description = "Multi-tenant API para gestión de PDR (varilleros)",
        Contact = new() { Name = "Varilleros Support" }
    });

    options.OperationFilter<TenantHeaderOperationFilter>();

    options.AddSecurityDefinition("TenantId", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-Tenant-Id",
        Description = "Introduce el slug del tenant (ej: acme, varilleros-bcn)"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Token JWT obtenido en POST /api/auth/login"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "TenantId" }
            },
            Array.Empty<string>()
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// --- CORS ---
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    {
        var origins = (allowedOrigins is { Length: > 0 })
            ? allowedOrigins
            : new[] { "http://localhost:4200" };
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    }));

// --- JWT Authentication ---
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("Jwt:Secret no configurado");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer   = true,
            ValidIssuer      = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience    = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
        };
    });

// --- Master DB factory ---
var masterConnStr = builder.Configuration.GetConnectionString("MasterDb")
    ?? throw new InvalidOperationException("ConnectionString 'MasterDb' no encontrada.");
builder.Services.AddSingleton<IMasterDbConnectionFactory>(new MySqlConnectionFactory(masterConnStr));

// --- Multi-tenancy ---
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TenantContext>();
builder.Services.AddScoped<TenantContextHolder>();
builder.Services.AddSingleton<ITenantResolver, TenantResolver>();
builder.Services.Configure<TenantCacheOptions>(builder.Configuration.GetSection("TenantCache"));

// --- Tenant DB factory ---
builder.Services.AddScoped<ITenantDbConnectionFactory, TenantDbConnectionFactory>();

// --- Repositorios Tenant ---
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPeritosRepository, PeritosRepository>();
builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>();
builder.Services.AddScoped<IPreciosRepository, PreciosRepository>();
builder.Services.AddScoped<IPresupuestoRepository, PresupuestoRepository>();

// --- Repositorios Master ---
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IModuleCatalogRepository, ModuleCatalogRepository>();
builder.Services.AddScoped<ITenantModuleRepository, TenantModuleRepository>();

// --- Use Cases: Auth ---
builder.Services.AddScoped<LoginUseCase>();

// --- Use Cases: Clientes ---
builder.Services.AddScoped<GetAllClientesUseCase>();
builder.Services.AddScoped<GetClienteByIdUseCase>();
builder.Services.AddScoped<CreateClienteUseCase>();
builder.Services.AddScoped<UpdateClienteUseCase>();
builder.Services.AddScoped<DeleteClienteUseCase>();

// --- Use Cases: Peritos ---
builder.Services.AddScoped<GetAllPeritosUseCase>();
builder.Services.AddScoped<GetPeritoByIdUseCase>();
builder.Services.AddScoped<CreatePeritoUseCase>();
builder.Services.AddScoped<UpdatePeritoUseCase>();
builder.Services.AddScoped<DeletePeritoUseCase>();

// --- Use Cases: Artículos ---
builder.Services.AddScoped<GetAllArticulosUseCase>();
builder.Services.AddScoped<GetArticuloByIdUseCase>();
builder.Services.AddScoped<CreateArticuloUseCase>();
builder.Services.AddScoped<UpdateArticuloUseCase>();
builder.Services.AddScoped<DeleteArticuloUseCase>();

// --- Use Cases: Precios ---
builder.Services.AddScoped<GetAllPreciosUseCase>();
builder.Services.AddScoped<GetPrecioByIdUseCase>();
builder.Services.AddScoped<CreatePrecioUseCase>();
builder.Services.AddScoped<UpdatePrecioUseCase>();
builder.Services.AddScoped<DeletePrecioUseCase>();

// --- Use Cases: Presupuestos ---
builder.Services.AddScoped<GetAllPresupuestosUseCase>();
builder.Services.AddScoped<GetPresupuestoByIdUseCase>();
builder.Services.AddScoped<CreatePresupuestoUseCase>();
builder.Services.AddScoped<UpdatePresupuestoUseCase>();
builder.Services.AddScoped<DeletePresupuestoUseCase>();

// --- Use Cases: Tenants (Admin) ---
builder.Services.AddScoped<GetAllTenantsUseCase>();
builder.Services.AddScoped<GetTenantByIdUseCase>();
builder.Services.AddScoped<CreateTenantUseCase>();
builder.Services.AddScoped<UpdateTenantUseCase>();
builder.Services.AddScoped<DeleteTenantUseCase>();

// --- Use Cases: Modules (Admin) ---
builder.Services.AddScoped<GetAllModulesUseCase>();
builder.Services.AddScoped<CreateModuleUseCase>();

// --- Use Cases: TenantModules (Admin) ---
builder.Services.AddScoped<GetTenantModulesUseCase>();
builder.Services.AddScoped<AssignModuleToTenantUseCase>();
builder.Services.AddScoped<RevokeModuleFromTenantUseCase>();

// --- Validadores (FluentValidation) ---
builder.Services.AddValidatorsFromAssemblyContaining<CreateClienteDtoValidator>();

var app = builder.Build();

// --- Pipeline ---
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors();
app.UseAuthentication();
app.UseMiddleware<TenantMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Varilleros API v1"));
}

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
