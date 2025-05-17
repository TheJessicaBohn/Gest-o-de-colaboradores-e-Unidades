using Gestao_de_Colaboradores_e_Unidades.Context;
using Gestao_de_Colaboradores_e_Unidades.Models;
using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;

namespace Gestao_de_Colaboradores_e_Unidades.Repository;

public class UsuariosRepository : IUsuariosRepository
{
    private readonly AppDbContext _context;
    public UsuariosRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UsuariosModel> Usuarios => throw new NotImplementedException();
    public IEnumerable<UsuariosModel> UsuariosAtivos => _context.Usuarios.
                                                        Where(u => u.UsuarioStatus);


    public UsuariosModel GetUsuarioById(string id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        if (usuario == null) throw new Exception("Usuario não encontado");

        return usuario; 
    }
}