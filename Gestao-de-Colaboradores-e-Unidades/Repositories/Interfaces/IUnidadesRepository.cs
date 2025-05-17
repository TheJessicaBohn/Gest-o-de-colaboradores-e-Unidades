using Gestao_de_Colaboradores_e_Unidades.Models;

namespace Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;

public interface IUnidadesRepository
{
    IEnumerable<UnidadesModel> Unidades { get; }
    UnidadesModel GetUnidadesById(string unidadeId);
}