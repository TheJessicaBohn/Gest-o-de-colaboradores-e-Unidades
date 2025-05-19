using Gestao.Models;

namespace Gestao.Repositories.Interfaces;

public interface IColaboradoresRepository
{
    void CriarColaborador(ColaboradoresModel colaborador);
    void AtualizarColaborador(ColaboradoresModel colaborador);
    IEnumerable<ColaboradoresModel> Colaboradores { get; }
    ColaboradoresModel BuscaColaboradorPorId(int colaboradoesId);
    void RemoverColaborador(int id);
}