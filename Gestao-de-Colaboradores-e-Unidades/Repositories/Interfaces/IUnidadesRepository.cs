using Gestao_de_Colaboradores_e_Unidades.Models;

namespace Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;

public interface IUnidadesRepository
{
    void CriarUnidade(UnidadesModel unidade);
    IEnumerable<UnidadesModel> Unidades { get; }
    UnidadesModel BuscaUnidadesPorId(int unidadeId);
    void AtualizarUnidade(UnidadesModel unidade);
    void InativarUnidade(int id);
}