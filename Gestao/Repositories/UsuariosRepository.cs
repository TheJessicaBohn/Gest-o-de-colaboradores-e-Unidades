using Gestao.Context;
using Gestao.Models;
using Gestao.Repositories.Interfaces;

namespace Gestao.Repository;

public class UsuariosRepository : IUsuariosRepository
{
    private readonly AppDbContext _context;
    public UsuariosRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UsuariosModel> Usuarios => _context.Usuarios;
    public IEnumerable<UsuariosModel> UsuariosAtivos => _context.Usuarios.
                                                        Where(u => u.UsuarioStatus);

    public void CriarUsuario(UsuariosModel usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public void AtualizarUsuario(UsuariosModel usuario)
    {
        var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == usuario.UsuarioId);

        if (usuarioExistente == null)
            throw new Exception("Usuário não encontrado.");

        usuarioExistente.UsuarioSenha = usuario.UsuarioSenha;
        usuarioExistente.UsuarioStatus = usuario.UsuarioStatus;

        _context.SaveChanges();
    }

    public UsuariosModel BuscaUsuarioPorId(Guid id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        if (usuario == null) throw new Exception("Usuario não encontado");

        return usuario;
    }

    public IEnumerable<UsuariosModel> BuscaUsuariosPorStatus(bool status)
    {
        return _context.Usuarios.Where(u => u.UsuarioStatus == status);
    }


}