using Gestao_de_Colaboradores_e_Unidades.Context;
using Gestao_de_Colaboradores_e_Unidades.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao_de_Colaboradores_e_Unidades.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly AppDbContext _context;

     public LoginRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UsuariosModel> BuscaUsuarioPorEmailESenha(string email, string senha)
    {
        var usuarios = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioEmail == email && u.UsuarioSenha == senha && u.UsuarioStatus == true);

        if (usuarios != null)
        {
            return usuarios;
        }

        throw new Exception("Verifique se a senha ou e-mail estão corretos");
        
    }
}

