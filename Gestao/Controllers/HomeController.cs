using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gestao.Models;

namespace Gestao.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var token = HttpContext.Session.GetString("Token");
        // if (string.IsNullOrEmpty(token))
        //     return RedirectToAction("Login", "Login");

        // HttpContext.Session.Clear();
        // return RedirectToAction("Login", "Login");

        var model = new HomeModel { Nome = "Host", Email = "host@host.com" };
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
