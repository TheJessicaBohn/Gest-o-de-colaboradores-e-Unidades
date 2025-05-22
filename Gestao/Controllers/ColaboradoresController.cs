using Gestao.Models;
using Gestao.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Controllers;

public class ColaboradoresController : Controller
{
    private readonly IColaboradoresRepository _colaboradorRepository;
    private readonly IUnidadesRepository _unidadesRepository;

    public ColaboradoresController(IColaboradoresRepository colaboradoresRepository, IUnidadesRepository unidadesRepository)
    {
        _colaboradorRepository = colaboradoresRepository ?? throw new System.ArgumentNullException(nameof(colaboradoresRepository));
        _unidadesRepository = unidadesRepository ?? throw new System.ArgumentNullException(nameof(unidadesRepository));
    }

    [HttpGet]
    public async Task<IActionResult> ListarColaboradores()
    {
        var listarColaboradores = await _colaboradorRepository.BuscarTodosOsColaboradores();
        return View(listarColaboradores);
    }

    [HttpGet]
    public async Task<IActionResult> Detalhes(int id)
    {
        var colaborador = await Task.FromResult(_colaboradorRepository.BuscaColaboradorPorId(id));
        if (colaborador == null)
            return NotFound();

        return View(colaborador);
    }

    [HttpGet]
  
    public async Task<IActionResult> AbrirViewCriar()
    {
        var unidades = await _unidadesRepository.BuscaTodasUnidadesAtivas();

        if (unidades == null || !unidades.Any())
        {
            TempData["MensagemErro"] = "Você precisa cadastrar pelo menos uma unidade antes.";
            return RedirectToAction("ListarColaboradores");
        }

        ViewBag.Unidades = new SelectList(unidades, "UnidadeId", "UnidadeCodigo");
        return View();
    }


    public async Task<IActionResult> CriarColaborador(ColaboradoresModel colaborador)
    {
        foreach (var modelStateKey in ModelState.Keys)
        {
            var value = ModelState[modelStateKey];
            foreach (var error in value.Errors)
            {
                Console.WriteLine($"Erro em {modelStateKey}: {error.ErrorMessage}");
            }
        }

        if (ModelState.IsValid)
        {
            await _colaboradorRepository.CriarColaborador(colaborador);
            return RedirectToAction(nameof(ListarColaboradores));
        }

        var unidades = _unidadesRepository.Unidades;
        ViewData["Unidades"] = new SelectList(unidades, "UnidadeId", "UnidadeCodigo");

        return View("AbrirViewCriar",colaborador);
    }


    [HttpGet]
    public async Task<IActionResult> AbrirViewEditar(int id)
    {
        var colaborador = await Task.FromResult(_colaboradorRepository.BuscaColaboradorPorId(id));
        if (colaborador == null)
            return NotFound();

        var unidades = await Task.FromResult(_unidadesRepository.Unidades);
        ViewData["UnidadeId"] = new SelectList(unidades, "UnidadeId", "UnidadeCodigo", colaborador.UnidadeId);
        return View(colaborador);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AtualizarColaborador(ColaboradoresModel colaborador)
    {
        if (colaborador.ColaboradorId!= colaborador.ColaboradorId)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await Task.Run(() => _colaboradorRepository.AtualizarColaborador(colaborador));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExisteColaborador(colaborador.ColaboradorId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(ListarColaboradores));
        }

        var unidades = await Task.FromResult(_unidadesRepository.Unidades);
        ViewData["UnidadeId"] = new SelectList(unidades, "UnidadeId", "UnidadeCodigo", colaborador.UnidadeId);
        return View(colaborador);
    }

    [HttpGet]
    public async Task<IActionResult> AbrirViewDeletar(int id)
    {
        var colaborador = await Task.FromResult(_colaboradorRepository.BuscaColaboradorPorId(id));
        if (colaborador == null)
            return NotFound();

        return View(colaborador);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletarColaborador(int id)
    {
        var colaborador = _colaboradorRepository.BuscaColaboradorPorId(id);
        if (colaborador == null)
        {
            return NotFound();
        }

        await _colaboradorRepository.RemoverColaborador(id);
        return RedirectToAction(nameof(ListarColaboradores));
    }

    private async Task<bool> ExisteColaborador(int id)
    {
        try
        {
            var colaborador = await Task.FromResult(_colaboradorRepository.BuscaColaboradorPorId(id));
            return colaborador != null;
        }
        catch
        {
            return false;
        }
    }
}