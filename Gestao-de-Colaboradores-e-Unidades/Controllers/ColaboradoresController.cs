using Gestao_de_Colaboradores_e_Unidades.Models;
using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Colaboradores_e_Unidades.Controllers;

public class ColaboradoresController : Controller
{
    private readonly IColaboradoresRepository _colaboradorRepository;

    public ColaboradoresController(IColaboradoresRepository colaboradoresRepository)
    {
        _colaboradorRepository = colaboradoresRepository;
    }

    [HttpGet]
    public IActionResult ListarColaboradores()
    {
        var listarColaboradores = _colaboradorRepository.Colaboradores;

        return View(listarColaboradores);
    }

    [HttpGet]
    public IActionResult Colaborador()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CriarColaborador(ColaboradoresModel colaborador)
    {

        if (colaborador == null)
        {
            ModelState.AddModelError("", "Você precisa preenchar os dados do colabrador");
        }

        if (ModelState.IsValid && colaborador != null)
        {
            _colaboradorRepository.CriarColaborador(colaborador);
        }

        ViewBag.ColaboradorCriadoComSucesso = "Colaborador criado com sucesso";

        return View("Index.cshtml");
    }
}
    