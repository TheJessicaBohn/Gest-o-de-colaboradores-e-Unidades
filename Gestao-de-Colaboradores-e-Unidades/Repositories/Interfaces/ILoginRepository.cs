using Gestao_de_Colaboradores_e_Unidades.Models;

namespace Gestao_de_Colaboradores_e_Unidades.Repositories;
public interface ILoginRepository
{
    Task<UsuariosModel> BuscaUsuarioPorEmailESenha(string email, string senha);
}
