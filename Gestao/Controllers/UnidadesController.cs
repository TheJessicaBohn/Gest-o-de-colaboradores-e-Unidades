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
public async Task<IActionResult> CriarUnidade(UnidadesModel unidade)
{
    if (!ModelState.IsValid)
    {
        return View("AbrirViewCriar", unidade); // 👈 troca aqui
    }

    var jaExiste = await _unidadesRepository.BuscaUnidadesPorCodigoAsync(unidade.UnidadeCodigo);
    if (jaExiste != null)
    {
        ModelState.AddModelError("UnidadeCodigo", "Já existe uma unidade com esse código.");
        return View("AbrirViewCriar", unidade); // 👈 e aqui também
    }

    _unidadesRepository.CriarUnidade(unidade);

    return RedirectToAction("ListarUnidades");
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

