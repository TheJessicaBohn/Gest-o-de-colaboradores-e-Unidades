using Gestao.Context;
using Gestao.Models;
using Microsoft.EntityFrameworkCore;

public class UsuariosRepository : IUsuariosRepository
{
    private readonly AppDbContext _context;

    public UsuariosRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UsuariosModel> Usuarios => _context.Usuarios;
    public IEnumerable<UsuariosModel> UsuariosAtivos => _context.Usuarios.Where(u => u.UsuarioStatus);

    public async Task<List<UsuariosModel>> BuscarTodosOsUsuarios()
    {
        var lista = await _context.Usuarios.ToListAsync();
        return lista;
    }
    public async Task CriarUsuarioAsync(UsuariosModel usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarUsuarioAsync(UsuariosModel usuario)
    {
        var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);
        if (usuarioExistente == null)
            throw new Exception("Usuário não encontrado.");

        usuarioExistente.UserName = usuario.UserName;
        usuarioExistente.NormalizedUserName = usuario.NormalizedUserName;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.UsuarioStatus = usuario.UsuarioStatus;

        await _context.SaveChangesAsync();
    }


    public async Task<UsuariosModel> BuscaUsuarioPorIdAsync(string id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario == null)
            throw new Exception("Usuário não encontrado.");

        return usuario;
    }

    public async Task<IEnumerable<UsuariosModel>> BuscaUsuariosPorStatusAsync(bool status)
    {
        return await Task.FromResult(_context.Usuarios.Where(u => u.UsuarioStatus == status));
    }

    public async Task<bool> UsuarioExisteAsync(string id)
    {
        return await _context.Usuarios.AnyAsync(e => e.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
