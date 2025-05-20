using Microsoft.AspNetCore.Mvc;
using Gestao.Models;
using System.Threading.Tasks;

namespace Gestao.Controllers;

public class UnidadesController : Controller
{
    private readonly IUnidadesRepository _unidadesRepository;

    public UnidadesController(IUnidadesRepository unidadesRepository)
    {
        _unidadesRepository = unidadesRepository;
    }

    [HttpGet]
    public async Task<IActionResult> ListarUnidades()
    {
        var unidades = await _unidadesRepository.BuscaTodasUnidadesAtivas();
        return View(unidades);
    }

    public IActionResult Detalhes(int? id)
    {
        if (id == null) return NotFound();

        var unidade = _unidadesRepository.BuscaUnidadesPorId(id.Value);
        if (unidade == null) return NotFound();

        return View(unidade);
    }

    public IActionResult AbrirViewCriar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CriarUnidade([Bind("UnidadeId,UnidadeCodigo,UnidadeNome,EstaUnidadeAtiva")] UnidadesModel unidadesModel)
    {
        if (ModelState.IsValid)
        {
            _unidadesRepository.CriarUnidade(unidadesModel);
            return RedirectToAction(nameof(ListarUnidades));
        }

        return View(unidadesModel);
    }

    public IActionResult AbrirViewEditar(int? id)
    {
        if (id == null) return NotFound();

        var unidade = _unidadesRepository.BuscaUnidadesPorId(id.Value);
        if (unidade == null) return NotFound();

        return View(unidade);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AtualizarUnidade([Bind("UnidadeId,UnidadeCodigo,UnidadeNome,EstaUnidadeAtiva")] UnidadesModel unidadesModel)
    {
        if (!ExisteUnidade(unidadesModel.UnidadeId))
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _unidadesRepository.AtualizarUnidade(unidadesModel);
                return RedirectToAction(nameof(ListarUnidades));
            }
            catch (Exception)
            {
                return View(unidadesModel);
            }
        }

        return View(unidadesModel);
    }


    public IActionResult AbrirViewDeletar(int? id)
    {
        if (id == null) return NotFound();

        var unidade = _unidadesRepository.BuscaUnidadesPorId(id.Value);
        if (unidade == null) return NotFound();

        return View(unidade);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletarUnidade(int id)
    {
        var unidade = _unidadesRepository.BuscaUnidadesPorId(id);
        if (unidade != null)
        {
            _unidadesRepository.InativarUnidade(id);
        }

        return RedirectToAction(nameof(ListarUnidades));
    }

    private bool ExisteUnidade(int id)
    {
        return _unidadesRepository.Unidades.Any(u => u.UnidadeId == id);
    }
}

