using Gestao_de_Colaboradores_e_Unidades.Models;
using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Colaboradores_e_Unidades.Controllers
{
    public class ColaboradoresController : Controller
    {
        private readonly IColaboradoresRepository _colaboradorRepository;
        private readonly IUnidadesRepository _unidadesRepository;

        public ColaboradoresController(IColaboradoresRepository colaboradoresRepository, IUnidadesRepository unidadesRepository)
        {
            _colaboradorRepository = colaboradoresRepository;
            _unidadesRepository = unidadesRepository;
        }

        [HttpGet]
        public IActionResult ListarColaboradores()
        {
            var colaboradores = _colaboradorRepository.Colaboradores;
            return View(colaboradores);
        }

        [HttpGet]
        public IActionResult Colaborador()
        {
            ViewBag.Unidades = _unidadesRepository.Unidades;
            return View();
        }

        [HttpPost]
       public IActionResult CriarColaborador(ColaboradoresModel? colaborador)
       {
            if (colaborador == null)
            {
                ModelState.AddModelError("", "Preencha os dados do colaborador.");
                ViewBag.Unidades = _unidadesRepository.Unidades;
                return View("Colaborador", new ColaboradoresModel { UnidadeId = 0 }); 
            }

            if (ModelState.IsValid)
            {
                _colaboradorRepository.CriarColaborador(colaborador);
                TempData["MensagemSucesso"] = "Colaborador criado com sucesso!";
                return RedirectToAction("ListarColaboradores");
            }

            ViewBag.Unidades = _unidadesRepository.Unidades;
            return View("Colaborador", colaborador);
       }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var colaborador = _colaboradorRepository.BuscaColaboradorPorId(id);
            if (colaborador == null) return NotFound();

            ViewBag.Unidades = _unidadesRepository.Unidades;
            return View(colaborador);
        }

        [HttpPost]
        public IActionResult Editar(ColaboradoresModel colaborador)
        {
            if (ModelState.IsValid)
            {
                _colaboradorRepository.AtualizarColaborador(colaborador);
                TempData["MensagemSucesso"] = "Colaborador atualizado com sucesso!";
                return RedirectToAction("ListarColaboradores");
            }

            ViewBag.Unidades = _unidadesRepository.Unidades;
            return View(colaborador);
        }

        // Deletar
        [HttpGet]
        public IActionResult Deletar(int id)
        {
            var colaborador = _colaboradorRepository.BuscaColaboradorPorId(id);
            if (colaborador == null) return NotFound();

            return View(colaborador);
        }

        [HttpPost]
        public IActionResult ConfirmarDeletar(int id)
        {
            _colaboradorRepository.RemoverColaborador(id);
            TempData["MensagemSucesso"] = "Colaborador removido com sucesso!";
            return RedirectToAction("ListarColaboradores");
        }
    }
}
