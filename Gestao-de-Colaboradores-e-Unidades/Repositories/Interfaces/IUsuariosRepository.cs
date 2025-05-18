using Gestao_de_Colaboradores_e_Unidades.Models;

namespace Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;

public interface IUsuariosRepository
{
    void CriarUsuario(UsuariosModel usuario);
    void AtualizarUsuario(UsuariosModel usuario);
    IEnumerable<UsuariosModel> Usuarios { get; }
    IEnumerable<UsuariosModel> UsuariosAtivos { get; }
    UsuariosModel BuscaUsuarioPorId(string usuarioId);
    IEnumerable<UsuariosModel> BuscaUsuariosPorStatus(bool status);
}