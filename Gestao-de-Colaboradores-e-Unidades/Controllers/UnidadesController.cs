using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Colaboradores_e_Unidades.Controllers;

public class UnidadesController : Controller
{
    private readonly IUnidadesRepository _unidadesRepository;

    public UnidadesController(IUnidadesRepository unidadesRepository)
    {
        _unidadesRepository = unidadesRepository;
    }

    public IActionResult ListarUnidades()
    {
        var listarUnidades = _unidadesRepository.Unidades;

        return View(listarUnidades);
    }
}
    