using Gestao.Models;

namespace Gestao.Repositories.Interfaces;

public interface IUsuariosRepository
{
    void CriarUsuario(UsuariosModel usuario);
    void AtualizarUsuario(UsuariosModel usuario);
    IEnumerable<UsuariosModel> Usuarios { get; }
    IEnumerable<UsuariosModel> UsuariosAtivos { get; }
    UsuariosModel BuscaUsuarioPorId(Guid usuarioId);
    IEnumerable<UsuariosModel> BuscaUsuariosPorStatus(bool status);
}