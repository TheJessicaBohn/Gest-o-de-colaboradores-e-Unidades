using Gestao_de_Colaboradores_e_Unidades.Services;
using Gestao_de_Colaboradores_e_Unidades.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Colaboradores_e_Unidades.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (model.Username == "admin" && model.Password == "123456")
        {
            var token = _tokenService.GenerateToken(model.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}


