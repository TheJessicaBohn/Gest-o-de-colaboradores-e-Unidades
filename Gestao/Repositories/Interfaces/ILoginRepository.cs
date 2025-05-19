using Gestao.Models;

namespace Gestao.Repositories;
public interface ILoginRepository
{
    Task<UsuariosModel> BuscaUsuarioPorEmailESenha(string email, string senha);
}
