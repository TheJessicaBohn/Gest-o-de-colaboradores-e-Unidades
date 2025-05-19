using Gestao.Models;

namespace Gestao.Repositories.Interfaces;

public interface IUnidadesRepository
{
    void CriarUnidade(UnidadesModel unidade);
    IEnumerable<UnidadesModel> Unidades { get; }
    UnidadesModel BuscaUnidadesPorId(int unidadeId);
    void AtualizarUnidade(UnidadesModel unidade);
    void InativarUnidade(int id);
}