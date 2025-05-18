using Gestao_de_Colaboradores_e_Unidades.Services;
using Gestao_de_Colaboradores_e_Unidades.Models;
using Microsoft.AspNetCore.Mvc;
using Gestao_de_Colaboradores_e_Unidades.Repositories;

namespace Gestao_de_Colaboradores_e_Unidades.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginRepository _loginRepository;
    private readonly TokenService _tokenService;

    public AuthController(ILoginRepository loginRepository, TokenService tokenService)
    {
        _loginRepository = loginRepository;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var usuario = await _loginRepository.BuscaUsuarioPorEmailESenha(model.Email, model.Password);

        if (usuario == null)
            return Unauthorized("Credenciais inválidas.");

        var token = _tokenService.GenerateToken(usuario.UsuarioEmail);
        return Ok(new { token });
    }
}


