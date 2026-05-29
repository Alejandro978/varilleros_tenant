namespace Varilleros.src.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Auth;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(LoginUseCase login) : ControllerBase
{
    /// <summary>Login de tenant — devuelve JWT y módulos activos</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
    {
        var result = await login.ExecuteAsync(dto, ct);
        if (result is null)
            return Unauthorized(new { error = "Empresa o contraseña incorrectos" });

        return Ok(result);
    }
}
