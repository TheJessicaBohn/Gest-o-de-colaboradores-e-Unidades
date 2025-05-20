using Microsoft.AspNetCore.Mvc;
using Gestao.Models;

namespace Gestao.Controllers;

public class UnidadesController : Controller
{
    private readonly IUnidadesRepository _unidadesRepository;

    public UnidadesController(IUnidadesRepository unidadesRepository)
    {
        _unidadesRepository = unidadesRepository;
    }

    [HttpGet]
    public IActionResult ListarUnidades()
    {
        var unidades = _unidadesRepository.Unidades;
        return View(unidades);
    }

    public IActionResult Detalhes(int? id)
    {
        if (id == null) return NotFound();

        var unidade = _unidadesRepository.BuscaUnidadesPorId(id.Value);
        if (unidade == null) return NotFound();

        return View(unidade);
    }

    public IActionResult AbreViewCriar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("UnidadeId,UnidadeCodigo,UnidadeNome,EstaUnidadeAtiva")] UnidadesModel unidadesModel)
    {
        if (ModelState.IsValid)
        {
            _unidadesRepository.CriarUnidade(unidadesModel);
            return RedirectToAction(nameof(Index));
        }

        return View(unidadesModel);
    }

    public IActionResult AbreViewEditar(int? id)
    {
        if (id == null) return NotFound();

        var unidade = _unidadesRepository.BuscaUnidadesPorId(id.Value);
        if (unidade == null) return NotFound();

        return View(unidade);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AtualizarUnidade(int id, [Bind("UnidadeId,UnidadeCodigo,UnidadeNome,EstaUnidadeAtiva")] UnidadesModel unidadesModel)
    {
        if (id != unidadesModel.UnidadeId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _unidadesRepository.AtualizarUnidade(unidadesModel);
            }
            catch (Exception)
            {
                if (!ExisteUnidade(unidadesModel.UnidadeId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(unidadesModel);
    }

    public IActionResult AbreViewDeletar(int? id)
    {
        if (id == null) return NotFound();

        var unidade = _unidadesRepository.BuscaUnidadesPorId(id.Value);
        if (unidade == null) return NotFound();

        return View(unidade);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult Deletar(int id)
    {
        var unidade = _unidadesRepository.BuscaUnidadesPorId(id);
        if (unidade != null)
        {
            _unidadesRepository.InativarUnidade(id);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ExisteUnidade(int id)
    {
        return _unidadesRepository.Unidades.Any(u => u.UnidadeId == id);
    }
}

