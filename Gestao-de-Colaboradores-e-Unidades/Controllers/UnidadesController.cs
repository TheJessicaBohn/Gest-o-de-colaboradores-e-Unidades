using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Colaboradores_e_Unidades.Controllers;

public class UnidadesController : Controller
{
    private readonly IUnidadesRepository _usuarioRepository;

    public UnidadesController(IUsuariosRepository unidadesRepository)
    {
        _usuarioRepository = unidadesRepository;
    }

    public IActionResult ListarUnidades()
    {
        var listaDeUnidades = _usuarioRepository.Unidades;

        return View(listaDeUnidades);
    }
}
    