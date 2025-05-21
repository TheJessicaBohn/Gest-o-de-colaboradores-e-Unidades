using Gestao.Models;
using Microsoft.AspNetCore.Identity;

namespace Gestao.Data;

public static class SeedData
{
    public static async Task CriarUsuarioPadrao(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<UsuariosModel>>();

        string email = "host@host.com";
        string senha = "X*j$yqx5DFx8@";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new UsuariosModel
            {
                UserName = "Host",
                Email = email,
                EmailConfirmed = true,
                UsuarioStatus = true
            };

            var result = await userManager.CreateAsync(user, senha);

            if (!result.Succeeded)
            {
                throw new Exception("Erro ao criar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}