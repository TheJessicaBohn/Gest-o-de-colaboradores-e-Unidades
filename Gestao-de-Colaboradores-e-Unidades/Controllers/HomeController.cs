using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gestao_de_Colaboradores_e_Unidades.Models;

namespace Gestao_de_Colaboradores_e_Unidades.Controllers;

public class HomeController : Controller
{ 
    public IActionResult Index()
    {
        HomeModel home = new HomeModel();

        home.Nome = "Jéssica Bohn";
        home.Email = "jessicabohn74@gmail.com";

        return View(home);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
