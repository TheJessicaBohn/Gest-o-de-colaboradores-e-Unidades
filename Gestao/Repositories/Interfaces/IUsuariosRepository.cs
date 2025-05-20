using Gestao.Models;

public interface IUsuariosRepository
{
    IEnumerable<UsuariosModel> Usuarios { get; }
    IEnumerable<UsuariosModel> UsuariosAtivos { get; }

    Task<List<UsuariosModel>> BuscarTodosOsUsuarios();
    Task CriarUsuarioAsync(UsuariosModel usuario);
    Task AtualizarUsuarioAsync(UsuariosModel usuario);
    Task<UsuariosModel> BuscaUsuarioPorIdAsync(string id);
    Task<IEnumerable<UsuariosModel>> BuscaUsuariosPorStatusAsync(bool status);
    Task<bool> UsuarioExisteAsync(string id);
    Task SaveChangesAsync();
}
