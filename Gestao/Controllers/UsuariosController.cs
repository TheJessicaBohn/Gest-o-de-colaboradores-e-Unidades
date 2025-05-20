using Gestao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Controllers;

public class UsuariosController : Controller
{
    private readonly IUsuariosRepository _usuarioRepository;

    public UsuariosController(IUsuariosRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> ListarUsuarios(bool status)
    {
        var usuariosFiltrados = await _usuarioRepository.BuscaUsuariosPorStatusAsync(status);
        return View("ListarUsuarios", usuariosFiltrados);
    }

    [HttpGet]
    public async Task<IActionResult> Detalhes(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        try
        {
            var usuario = await _usuarioRepository.BuscaUsuarioPorIdAsync(id);
            return View(usuario);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult AbrirViewCriar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Criar([Bind("UsuarioStatus,Id,UserName,NormalizedUserName,Email")] UsuariosModel usuariosModel)
    {
        if (ModelState.IsValid)
        {
            await _usuarioRepository.CriarUsuarioAsync(usuariosModel);
            return RedirectToAction(nameof(ListarUsuarios), new { status = true });
        }
        return View(usuariosModel);
    }

    [HttpGet]
    public async Task<IActionResult> AbrirViewEditar(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        try
        {
            var usuario = await _usuarioRepository.BuscaUsuarioPorIdAsync(id);
            return View(usuario);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Editar(string id, [Bind("UsuarioStatus,Id,UserName,NormalizedUserName,Email")] UsuariosModel usuariosModel)
    {
        if (id != usuariosModel.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _usuarioRepository.AtualizarUsuarioAsync(usuariosModel);
                return RedirectToAction(nameof(ListarUsuarios), new { status = true });
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _usuarioRepository.UsuarioExisteAsync(usuariosModel.Id);
                if (!exists)
                    return NotFound();
                throw;
            }
        }
        return View(usuariosModel);
    }
}
