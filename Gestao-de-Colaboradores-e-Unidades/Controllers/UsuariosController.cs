using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Colaboradores_e_Unidades.Controllers;

public class UsuariosController : Controller
{
    private readonly IUsuariosRepository _usuarioRepository;

    public UsuariosController(IUsuariosRepository usuariosRepository)
    {
        _usuarioRepository = usuariosRepository;
    }

    [HttpGet]
    public IActionResult ListarUsuarios()
    {
        var listaDeUsuarios = _usuarioRepository.Usuarios;

        return View(listaDeUsuarios);
    }

    [HttpGet]
    public IActionResult Usuarios()
    {
        return View();
    }
}
    