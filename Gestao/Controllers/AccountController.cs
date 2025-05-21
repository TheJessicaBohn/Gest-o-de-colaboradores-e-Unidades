using Gestao.Services;
using Microsoft.AspNetCore.Mvc;
using Gestao.Repositories;
using Microsoft.AspNetCore.Identity;
using Gestao.ViewModels;
using Gestao.Models;

namespace Gestao.Controllers;

[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly SignInManager<UsuariosModel> _signInManager;
    private readonly UserManager<UsuariosModel> _userManager;

    public AccountController(SignInManager<UsuariosModel> signInManager, UserManager<UsuariosModel> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = "/")
    {
        return View(new LoginViewModel()
        {
            NomeUsuario = String.Empty,
            Senha = String.Empty,
            RetornaURL = returnUrl
        });
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, [FromServices] TokenService tokenService)
    {
        var user = await _userManager.FindByEmailAsync(model.NomeUsuario);

        if (user != null)
        {
            var resultado = await _signInManager.PasswordSignInAsync(user, model.Senha, false, false);
            if (resultado.Succeeded)
            {
                if (user != null)
                {
                    var token = tokenService.GenerateToken(user.Email);
                    HttpContext.Session.SetString("Token", token);

                    if (string.IsNullOrEmpty(model.RetornaURL))
                        return RedirectToAction("Index", "Home");

                    return RedirectToAction(model.RetornaURL);
                }
            }
        }

            ModelState.AddModelError("", "Falha ao realizar o login");
            return View(model);

        }

}

