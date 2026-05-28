namespace Varilleros.src.Api.Middleware;

using FluentValidation;
using Varilleros.src.Domain.Exceptions;

public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await next(ctx);
        }
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
                error = "Validación fallida",
                errors = ex.Errors.Select(e => e.ErrorMessage).ToList()
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
