using Gestao.Services;
using Microsoft.AspNetCore.Mvc;
using Gestao.Repositories;
using Microsoft.AspNetCore.Identity;
using Gestao.ViewModels;

namespace Gestao.Controllers;

[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
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
    public async Task<IActionResult> Login(LoginViewModel model, [FromServices] ILoginRepository loginRepo, [FromServices] TokenService tokenService)
    {
        var user = await _userManager.FindByEmailAsync(model.NomeUsuario);
        if (user != null)
        {
            var resultado = await _signInManager.PasswordSignInAsync(user, model.Senha, false, false);
            if (resultado.Succeeded)
            {
                if (string.IsNullOrEmpty(model.RetornaURL))
                {
                    return RedirectToAction("Index", "Home");
                }

                var token = tokenService.GenerateToken(user.UserName);
                HttpContext.Session.SetString("Token", token);
                return RedirectToAction(model.RetornaURL);
            }
    
        }

        ModelState.AddModelError("", "Falha ao realizar o login");
        return View(model);
    }
}

