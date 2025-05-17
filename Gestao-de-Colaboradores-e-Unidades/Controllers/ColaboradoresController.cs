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

    public IActionResult ListarColaboradores()
    {
        var listaDeColaboradores = _colaboradorRepository.Colaboradores;

        return View(listaDeColaboradores);
    }
}
    