using Gestao.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Gestao.Models;

namespace Gestao.Controllers;

public class UsuariosController : Controller
{
    private readonly IUsuariosRepository _usuarioRepository;

    public UsuariosController(IUsuariosRepository usuariosRepository)
    {
        _usuarioRepository = usuariosRepository;
    }

    [HttpGet]
    public IActionResult ListarUsuarios(bool status)
    {
        var usuariosFiltrados = _usuarioRepository.BuscaUsuariosPorStatus(status);
        return View("ListarUsuarios", usuariosFiltrados);
    }

    [HttpGet]
    public IActionResult AbrirFormularioDeCadastro()
    {
        return View("Usuario", new UsuariosModel());
    }

    [HttpPost]
    public IActionResult Cadastrar(UsuariosModel usuario)
    {
        if (ModelState.IsValid)
        {
            _usuarioRepository.CriarUsuario(usuario);
            return RedirectToAction("ListarUsuarios");
        }

        return View("Usuario", usuario);

    }

    [HttpGet]
    public IActionResult AbrirFormularioDeEditar(Guid id)
    {
        var usuario = _usuarioRepository.BuscaUsuarioPorId(id);

        if (usuario == null) return NotFound();

        return View("Usuario", usuario);
    }


    [HttpPost]
    public IActionResult Editar(Guid id, [Bind("UsuarioId,UsuarioSenha,UsuarioStatus")] UsuariosModel usuarioAtualizado)
    {
        if (ModelState.IsValid)
        {
            var usuarioExistente = _usuarioRepository.BuscaUsuarioPorId(id);

            if (usuarioExistente == null) return NotFound();

            usuarioExistente.UsuarioSenha = usuarioAtualizado.UsuarioSenha;
            usuarioExistente.UsuarioStatus = usuarioAtualizado.UsuarioStatus;

            _usuarioRepository.AtualizarUsuario(usuarioExistente);

            return RedirectToAction("ListarUsuarios");
        }

        return View(usuarioAtualizado);
    }
}
