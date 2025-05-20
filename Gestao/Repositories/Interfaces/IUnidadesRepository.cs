using Gestao.Models;

public interface IUnidadesRepository
{
    IEnumerable<UnidadesModel> Unidades { get; }
    void CriarUnidade(UnidadesModel unidade);
    UnidadesModel BuscaUnidadesPorId(int id);
    void AtualizarUnidade(UnidadesModel unidade);
    void InativarUnidade(int id);
    public Task<IEnumerable<UnidadesModel>> BuscaTodasUnidadesAtivas();
}
