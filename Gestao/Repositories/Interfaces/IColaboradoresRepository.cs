using Gestao.Models;

namespace Gestao.Repositories.Interfaces;

public interface IColaboradoresRepository
{
    Task<List<ColaboradoresModel>> BuscarTodosOsColaboradores();
    Task<bool> CriarColaborador(ColaboradoresModel colaborador);
    void AtualizarColaborador(ColaboradoresModel colaborador);
    ColaboradoresModel BuscaColaboradorPorId(int colaboradoesId);
    Task RemoverColaborador(int id);
}